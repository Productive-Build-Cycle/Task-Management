using Application;
using InfraStructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

#region Add Dependecies

builder.Services
    .RegisterApplicationConfigurations()
    .RegisterInfraStructureConfigurations(builder.Configuration)
    ;

#endregion Add Dependecies

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();

    app.MapOpenApi();
}

builder.Services.AddCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// Created from develop for mentor review per instructions.
// The team is actively continuing development.
// Remaining tasks are broken down on the board and marked with TODOs in the code for ongoing implementation.
