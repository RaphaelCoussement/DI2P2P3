namespace WebApplication1;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiKey;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _apiKey = configuration.GetValue<string>("ApiKey") ?? "";
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("x-api-key", out var extractedApiKey) || 
            extractedApiKey != _apiKey)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key non valide !");
            return;
        }

        await _next(context);
    }
}
