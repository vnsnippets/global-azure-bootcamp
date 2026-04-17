var builder = WebApplication.CreateBuilder(args);

// Configure CORS so your Blazor app (likely port 5001 or 7001) can talk to this server
builder.Services.AddCors(options =>
{
    options.AddPolicy("SnakePolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5295", "https://localhost:7233") // Add your Blazor URLs
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // SignalR REQUIRES this
    });
});

// Add SignalR and bind it to Azure
builder.Services.AddSignalR()
                .AddAzureSignalR(builder.Configuration["Azure:SignalR:ConnectionString"]);

var app = builder.Build();

// 3. Middleware Order is Critical!
app.UseRouting();

app.UseCors("SnakePolicy");

// 4. Map the Hub endpoint
app.MapHub<GameHub>("/gamehub");

app.Run();