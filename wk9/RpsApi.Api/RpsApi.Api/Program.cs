using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using RpsApi.DataStorage;
using RpsApi.DataStorage.Model;
using RpsApi.Logic;

//string connectionString = await File.ReadAllTextAsync("C:/revature/DEMO_RPS_CS.txt");

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("RPS-DB-Connection");

// key would be "Logging:LogLevel:Default" for that setting, ":" is the heirarchical nesting
bool prettyPrintJson = builder.Configuration.GetValue<string>("PrettyPrintJsonOutput") == "true";

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // custom formatters configured here to enable any/all action methods to either
    // get new/different/non-json formats in model binding and/or their results
    //  to get serialized in new/different/non-json formats
    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());

    var jsonFormatter = options.OutputFormatters.OfType<SystemTextJsonOutputFormatter>().First();
    jsonFormatter.SerializerOptions.WriteIndented = prettyPrintJson;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// provide any other dependencies that, e.g., the controllers need injected into them

// set up our context for dependency injection
// in "scoped" lifetime by default, not singleton,
//   which means, a new instance will be made of each HTTP request that is handled.
//   that's good because DbContext is not threadsafe.
builder.Services.AddDbContext<RPSContext>(options =>
{
    // logging to console is on by default
    options.UseSqlServer(connectionString);
});

builder.Services.AddSingleton<IMoveDecider, RandomMoveDecider>();

if (builder.Configuration.GetValue<string>("UseEf") == "true")
{
    // because the context is scoped lifetime, the other service that needs it (EfRepository)
    //   also needs to be scoped lifetime (or transient).
    // (don't need a delegate here to tell it how to make the class, it can figure it out in this case
    builder.Services.AddScoped<IRepository, EfRepository>();
}
else
{
    // "if anyone asks for an IRepository, run this lambda expression"
    // (uses a service provider parameter to grab another dependency it needs, from the same DI container)
    builder.Services.AddSingleton<IRepository>(sp => new SqlRepository(connectionString, sp.GetRequiredService<ILogger<SqlRepository>>()));
}

builder.Services.AddCors(options =>
{
    // here you put all the origins that websites making requests to this API via JS are hosted at
    options.AddDefaultPolicy(builder =>
        builder
            .WithOrigins("http://127.0.0.1:5500",
                         "https://my-example-website.azurewebsites.net")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger document aka OpenAPI document

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
