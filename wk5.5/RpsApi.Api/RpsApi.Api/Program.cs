using RpsApi.DataStorage;

string connectionString = await File.ReadAllTextAsync("C:/revature/richard-rps-db.txt");
IRepository repository = new SqlRepository(connectionString);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
