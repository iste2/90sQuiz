using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using Quiz90s.Hubs;

namespace Quiz90s.GameData
{
    public class Game
    {
        private readonly Dictionary<string, Player> _players = new Dictionary<string, Player>();
        public string GameId { get; }
        private readonly IGroupManager _groupManager;
        private readonly IClientProxy _clientProxy;

        private bool GameStarted { get; set; } = false;

        public Game(string gameId, IGroupManager groupManager, IClientProxy clientProxy)
        {
            GameId = gameId;
            _groupManager = groupManager;
            _clientProxy = clientProxy;
        }

        public async void AddPlayer(Player player, string contextConnectionId)
        {
            if (GameStarted)
            {
                await _clientProxy.SendAsync(Actions.JoinGameFail.ToString(), $"Beitritt von Spieler {player.Name} zu Spiel {GameId} fehlgeschlagen. Spiel wurde bereits gestartet.");
                return;
            }
            
            _players[player.PlayerId] = player;
            await _groupManager.AddToGroupAsync(contextConnectionId, GameId);
            await _clientProxy.SendAsync(Actions.JoinGameSuccess.ToString(), $"Spieler {player.Name} tritt Spiel {GameId} bei.");
        }

    }
}

