using System;

namespace EventFramework.Core {
    public interface IEventListener<in T> where T : IEvent {
        void OnEvent(T evt);
    }
} 