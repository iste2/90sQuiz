namespace Quiz90s.GameData
{
    public class Game
    {
        private readonly Dictionary<string, Player> _players = new Dictionary<string, Player>();
        private string _gameId;

        public Game(string gameId)
        {
            _gameId = gameId;
        }

        public void AddPlayer(Player player)
        {
            _players[player.PlayerId] = player;
        }

    }
}

