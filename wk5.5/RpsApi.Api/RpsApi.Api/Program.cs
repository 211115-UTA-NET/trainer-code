using Microsoft.AspNetCore.Mvc.Formatters;
using RpsApi.DataStorage;

string connectionString = await File.ReadAllTextAsync("C:/revature/richard-rps-db.txt");

var builder = WebApplication.CreateBuilder(args);

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
// "if anyone asks for an IRepository, run this lambda expression"
// (uses a service provider parameter to grab another dependency it needs, from the same DI container)
builder.Services.AddSingleton<IRepository>(sp => new SqlRepository(connectionString, sp.GetRequiredService<ILogger<SqlRepository>>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger document aka OpenAPI document

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
