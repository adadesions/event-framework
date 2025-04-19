using System;
using EventFramework.Core;
using EventFramework.Bus;

namespace EventFramework.Extensions {
    public static class EventBusExtensions {
        public static void SubscribeToAll(this IEventMultiListener listener) {
            var interfaces = listener.GetType().GetInterfaces();
            foreach (var iface in interfaces) {
                if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IEventListener<>)) {
                    var method = typeof(EventBus).GetMethod("Subscribe")
                        .MakeGenericMethod(iface.GenericTypeArguments[0]);
                    Action<IEvent> wrapper = (e) => iface.GetMethod("OnEvent").Invoke(listener, new object[] { e });
                    method.Invoke(null, new object[] { wrapper });
                }
            }
        }

        public static void UnsubscribeFromAll(this IEventMultiListener listener) {
            var interfaces = listener.GetType().GetInterfaces();
            foreach (var iface in interfaces) {
                if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IEventListener<>)) {
                    var method = typeof(EventBus).GetMethod("Unsubscribe")
                        .MakeGenericMethod(iface.GenericTypeArguments[0]);
                    Action<IEvent> wrapper = (e) => iface.GetMethod("OnEvent").Invoke(listener, new object[] { e });
                    method.Invoke(null, new object[] { wrapper });
                }
            }
        }
    }
} 