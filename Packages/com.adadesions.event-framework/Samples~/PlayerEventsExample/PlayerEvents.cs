using EventFramework.Core;

namespace EventFramework.Examples {
    // Example of a simple event
    public class PlayerSpawnedEvent : IEvent {
        public string PlayerName { get; }
        public Vector3 Position { get; }

        public PlayerSpawnedEvent(string playerName, Vector3 position) {
            PlayerName = playerName;
            Position = position;
        }
    }

    // Example of an event with more data
    public class PlayerHealthChangedEvent : IEvent {
        public string PlayerName { get; }
        public float OldHealth { get; }
        public float NewHealth { get; }
        public float MaxHealth { get; }

        public PlayerHealthChangedEvent(string playerName, float oldHealth, float newHealth, float maxHealth) {
            PlayerName = playerName;
            OldHealth = oldHealth;
            NewHealth = newHealth;
            MaxHealth = maxHealth;
        }
    }

    // Example of an event with an enum
    public enum PlayerState {
        Idle,
        Walking,
        Running,
        Jumping,
        Falling
    }

    public class PlayerStateChangedEvent : IEvent {
        public string PlayerName { get; }
        public PlayerState OldState { get; }
        public PlayerState NewState { get; }

        public PlayerStateChangedEvent(string playerName, PlayerState oldState, PlayerState newState) {
            PlayerName = playerName;
            OldState = oldState;
            NewState = newState;
        }
    }
} 