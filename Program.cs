var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole(); // Enable console logging

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        throw;
    }
});

app.MapReverseProxy();
app.Run();
