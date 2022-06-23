using System;
using Microsoft.AspNetCore.SignalR.Client;
using Quiz90s.GameData;

namespace Quiz90s.State
{
    public class ClientState
    {
        private HubConnection? _hubConnection;
        private Player _player = new Player();
        public event Action? OnChange;

        public HubConnection? HubConnection
        {
            get => _hubConnection;
            set
            {
                _hubConnection = value;
                NotifyStateChanged();
            }
        }

        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                NotifyStateChanged();
            }
        }

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}