using Microsoft.AspNetCore.SignalR;

public class GameHub : Hub
{
    public async Task UpdateSnake(string playerId, List<Position> body, string color)
    {
        // Broadcast the body and color to everyone else
        await Clients.Others.SendAsync("ReceiveSnakeUpdate", playerId, body, color);
    }
}

public record Position(float X, float Z);