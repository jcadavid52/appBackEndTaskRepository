using TaskCli_Data;
using TaskCli_LogicBusiness;
using TaskCli_Services;
using TaskCli_Utils;

var builder = WebApplication.CreateBuilder(args);

IManagementFiles file = new ManagementFileJson();

var response = file.initializeFile();

IRepositoryFile repository = new RepositoryFileJson();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILogicApp> (provider =>
{
    return new LogicApp(repository);
});

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
