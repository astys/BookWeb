using BookWeb.API.Endpoints.Configuration;
using BookWeb.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration, typeof(IEndpointBase).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapSwagger();
    app.UseSwaggerUI();
}

app.MapEndpoints();
app.UseCors();

app.Run();