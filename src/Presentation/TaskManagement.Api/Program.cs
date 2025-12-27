using Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

#region Add Dependecies

builder.Services.RegisterApplicationConfigurations();

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
