using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using EventFramework.Core;

namespace EventFramework.Bus {
    public static class EventBus {
        private static readonly ConcurrentDictionary<Type, List<Delegate>> _listeners = new();
        private static readonly object _lock = new();

        public static void Subscribe<T>(Action<T> listener) where T : IEvent {
            var type = typeof(T);
            lock (_lock) {
                if (!_listeners.TryGetValue(type, out var list)) {
                    list = new List<Delegate>();
                    _listeners[type] = list;
                }
                if (!list.Contains(listener)) list.Add(listener);
            }
        }

        public static void Unsubscribe<T>(Action<T> listener) where T : IEvent {
            var type = typeof(T);
            lock (_lock) {
                if (_listeners.TryGetValue(type, out var list)) {
                    list.Remove(listener);
                    if (list.Count == 0) _listeners.TryRemove(type, out _);
                }
            }
        }

        public static void Publish<T>(T evt) where T : IEvent {
            var type = typeof(T);
            List<Delegate> snapshot;
            lock (_lock) {
                if (!_listeners.TryGetValue(type, out var list)) return;
                snapshot = new List<Delegate>(list);
            }
            foreach (var listener in snapshot) {
                try {
                    ((Action<T>)listener)?.Invoke(evt);
                } catch (Exception ex) {
                    Debug.LogError($"[EventBus] Exception in {type.Name} listener: {ex.Message}");
                }
            }
        }

        public static void ClearAll() {
            lock (_lock) {
                _listeners.Clear();
            }
        }
    }
} 