using Microsoft.AspNetCore.SignalR;
using Quiz90s.GameData;

namespace Quiz90s.Hubs
{
    public class GameHub : Hub
    {
        private Dictionary<string, Game> _games = new Dictionary<string, Game>();

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinGame(Player player, string gameId) 
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            if (!_games.TryGetValue(gameId, out var hGame))
            {
                hGame = new Game(gameId);
                _games.Add(gameId, hGame);
            }
            hGame.AddPlayer(player);
        }

        public async Task LeaveGame(string userId, string gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
        }
    }
}