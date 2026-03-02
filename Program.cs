using CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGuidGenerator, GuidGenerator>();

var app = builder.Build();

app.UseTransactionMiddleware();

app.Map("/", (HttpContext context) =>
{
    return Results.Text("Hello there!");
});

app.Run();
