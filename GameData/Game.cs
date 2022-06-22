using Microsoft.AspNetCore.SignalR;
using Quiz90s.Hubs;

namespace Quiz90s.GameData
{
    public class Game
    {
        private readonly Dictionary<string, Player> _players = new Dictionary<string, Player>();
        private string _gameId;
        private readonly IGroupManager _groupManager;
        private readonly IClientProxy _clientProxy;

        public Game(string gameId, IGroupManager groupManager, IClientProxy clientProxy)
        {
            _gameId = gameId;
            _groupManager = groupManager;
            _clientProxy = clientProxy;
        }

        public async void AddPlayer(Player player, string contextConnectionId)
        {
            _players[player.PlayerId] = player;
            await _groupManager.AddToGroupAsync(contextConnectionId, _gameId);
            await _clientProxy.SendAsync(Actions.JoinGameSuccess.ToString(), player);
        }

    }
}

