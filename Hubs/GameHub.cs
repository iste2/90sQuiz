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

        public async Task JoinGame(Player player, string? gameId) 
        {
            Console.WriteLine(player.Name);
            Console.WriteLine(gameId);

            if (!string.IsNullOrEmpty(gameId) && !_games.ContainsKey(gameId))
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Game does not exist");
                return;
            }

            if (string.IsNullOrEmpty(gameId))
            {
                gameId = new Guid().ToString();
                _games.Add(gameId, new Game(gameId, Groups, Clients.Group(gameId)));
            }
            
            _games[gameId].AddPlayer(player, Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        }

        public async Task LeaveGame(string userId, string gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
        }
    }
}