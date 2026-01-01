using Application;
using InfraStructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services
    .RegisterApplicationConfigurations()
    .RegisterInfraStructureConfigurations(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

builder.Services.AddCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
