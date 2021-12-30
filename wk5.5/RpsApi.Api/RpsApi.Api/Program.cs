using Microsoft.AspNetCore.Mvc.Formatters;
using RpsApi.DataStorage;

string connectionString = await File.ReadAllTextAsync("C:/revature/richard-rps-db.txt");
IRepository repository = new SqlRepository(connectionString);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    // custom formatters configured here to enable any/all action methods to either
    // get new/different/non-json formats in model binding and/or their results
    //  to get serialized in new/different/non-json formats
    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// provide any other dependencies that, e.g., the controllers need injected into them
// "if anyone asks for an IRepository, give them this object"
builder.Services.AddSingleton<IRepository>(repository);

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
