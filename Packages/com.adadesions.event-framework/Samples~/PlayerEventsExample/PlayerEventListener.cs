using UnityEngine;
using EventFramework.Core;
using EventFramework.Bus;

namespace EventFramework.Examples {
    // Example of a simple listener for a specific event
    public class PlayerSpawnListener : IEventListener<PlayerSpawnedEvent> {
        public void OnEvent(PlayerSpawnedEvent evt) {
            Debug.Log($"Player {evt.PlayerName} spawned at position {evt.Position}");
        }
    }

    // Example of a listener that handles multiple events
    public class PlayerHealthListener : IEventListener<PlayerHealthChangedEvent> {
        public void OnEvent(PlayerHealthChangedEvent evt) {
            float healthPercentage = (evt.NewHealth / evt.MaxHealth) * 100f;
            Debug.Log($"Player {evt.PlayerName} health changed from {evt.OldHealth} to {evt.NewHealth} ({healthPercentage:F1}%)");
            
            // Example of conditional logic based on event data
            if (evt.NewHealth <= 0) {
                Debug.LogWarning($"Player {evt.PlayerName} has died!");
            } else if (evt.NewHealth < evt.OldHealth) {
                Debug.Log($"Player {evt.PlayerName} took damage!");
            } else if (evt.NewHealth > evt.OldHealth) {
                Debug.Log($"Player {evt.PlayerName} was healed!");
            }
        }
    }

    // Example of a listener that uses the IEventMultiListener interface
    public class PlayerStateListener : IEventMultiListener, 
        IEventListener<PlayerStateChangedEvent> {
        
        public void OnEvent(PlayerStateChangedEvent evt) {
            Debug.Log($"Player {evt.PlayerName} state changed from {evt.OldState} to {evt.NewState}");
            
            // Example of handling different states
            switch (evt.NewState) {
                case PlayerState.Idle:
                    Debug.Log($"Player {evt.PlayerName} is now idle");
                    break;
                case PlayerState.Walking:
                    Debug.Log($"Player {evt.PlayerName} is now walking");
                    break;
                case PlayerState.Running:
                    Debug.Log($"Player {evt.PlayerName} is now running");
                    break;
                case PlayerState.Jumping:
                    Debug.Log($"Player {evt.PlayerName} is now jumping");
                    break;
                case PlayerState.Falling:
                    Debug.Log($"Player {evt.PlayerName} is now falling");
                    break;
            }
        }
    }
} 