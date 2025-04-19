using UnityEngine;
using EventFramework.Core;
using EventFramework.Bus;
using EventFramework.Extensions;

namespace EventFramework.Examples {
    /// <summary>
    /// Example MonoBehaviour that demonstrates how to use the EventFramework in a Unity component.
    /// </summary>
    public class PlayerEventExample : MonoBehaviour {
        // Example listeners
        private PlayerSpawnListener _spawnListener;
        private PlayerHealthListener _healthListener;
        private PlayerStateListener _stateListener;
        
        // Example player data
        private string _playerName = "Player1";
        private float _health = 100f;
        private float _maxHealth = 100f;
        private PlayerState _currentState = PlayerState.Idle;
        
        private void Awake() {
            // Create listeners
            _spawnListener = new PlayerSpawnListener();
            _healthListener = new PlayerHealthListener();
            _stateListener = new PlayerStateListener();
            
            // Subscribe to events
            EventBus.Subscribe<PlayerSpawnedEvent>(_spawnListener.OnEvent);
            EventBus.Subscribe<PlayerHealthChangedEvent>(_healthListener.OnEvent);
            
            // Subscribe to all events for the multi-listener
            _stateListener.SubscribeToAll();
            
            // Publish initial spawn event
            PublishSpawnEvent();
        }
        
        private void OnDestroy() {
            // Unsubscribe from events
            EventBus.Unsubscribe<PlayerSpawnedEvent>(_spawnListener.OnEvent);
            EventBus.Unsubscribe<PlayerHealthChangedEvent>(_healthListener.OnEvent);
            
            // Unsubscribe from all events for the multi-listener
            _stateListener.UnsubscribeFromAll();
        }
        
        // Example methods that publish events
        private void PublishSpawnEvent() {
            var spawnEvent = new PlayerSpawnedEvent(_playerName, transform.position);
            EventBus.Publish(spawnEvent);
        }
        
        public void TakeDamage(float amount) {
            float oldHealth = _health;
            _health = Mathf.Max(0, _health - amount);
            
            var healthEvent = new PlayerHealthChangedEvent(_playerName, oldHealth, _health, _maxHealth);
            EventBus.Publish(healthEvent);
        }
        
        public void Heal(float amount) {
            float oldHealth = _health;
            _health = Mathf.Min(_maxHealth, _health + amount);
            
            var healthEvent = new PlayerHealthChangedEvent(_playerName, oldHealth, _health, _maxHealth);
            EventBus.Publish(healthEvent);
        }
        
        public void ChangeState(PlayerState newState) {
            PlayerState oldState = _currentState;
            _currentState = newState;
            
            var stateEvent = new PlayerStateChangedEvent(_playerName, oldState, newState);
            EventBus.Publish(stateEvent);
        }
        
        // Example input handling
        private void Update() {
            // Example: Press H to heal
            if (Input.GetKeyDown(KeyCode.H)) {
                Heal(20f);
            }
            
            // Example: Press D to take damage
            if (Input.GetKeyDown(KeyCode.D)) {
                TakeDamage(10f);
            }
            
            // Example: Press number keys to change state
            if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeState(PlayerState.Idle);
            if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeState(PlayerState.Walking);
            if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeState(PlayerState.Running);
            if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeState(PlayerState.Jumping);
            if (Input.GetKeyDown(KeyCode.Alpha5)) ChangeState(PlayerState.Falling);
        }
    }
} 