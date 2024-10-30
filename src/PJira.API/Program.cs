using PJira.Infrastructure;
using PJira.Application;
using PJira.API;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.AddAPIServices(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(conf =>
    {
        conf.RoutePrefix = string.Empty;
        conf.SwaggerEndpoint("swagger/v1/swagger.json", "PJira Api");
    });
}

app.Run();
