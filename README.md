# AdaBrain Event Framework

A reusable, thread-safe event system for Unity using pub/sub and code extensibility.

[![Unity Version](https://img.shields.io/badge/Unity-2022.3%2B-blue.svg)](https://unity.com/download)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## Overview

AdaBrain Event Framework is a lightweight, thread-safe event system for Unity that provides a simple and efficient way to implement the publish/subscribe pattern in your games. It's designed to be easy to use, performant, and extensible.

### Key Features

- **Thread-safe**: Built with thread safety in mind using `ConcurrentDictionary` and proper locking mechanisms
- **Type-safe**: Uses C# generics to ensure type safety at compile time
- **Extensible**: Easy to create custom events and listeners
- **Multi-listener support**: Listen to multiple events with a single class
- **Unity 2022.3+ compatible**: Works with Unity 2022.3 and newer versions
- **No dependencies**: Self-contained with no external dependencies

## Installation

### Via Unity Package Manager (Git)

1. Open the Unity Package Manager (Window > Package Manager)
2. Click the "+" button in the top-left corner
3. Select "Add package from git URL..."
4. Enter the following URL:
   ```
   https://github.com/adadesions/event-framework.git
   ```
5. Click "Add"

### Manual Installation

1. Download the latest release from the [Releases](https://github.com/adadesions/event-framework/releases) page
2. Extract the contents to your Unity project's `Packages` folder
3. The package should now be available in your project

## Usage

### Basic Usage

1. Create an event class that implements `IEvent`:

```csharp
public class MyEvent : IEvent {
    public string Message { get; }
    
    public MyEvent(string message) {
        Message = message;
    }
}
```

2. Create a listener that implements `IEventListener<MyEvent>`:

```csharp
public class MyListener : IEventListener<MyEvent> {
    public void OnEvent(MyEvent evt) {
        Debug.Log($"Received message: {evt.Message}");
    }
}
```

3. Subscribe to the event and publish it:

```csharp
// Subscribe
var listener = new MyListener();
EventBus.Subscribe<MyEvent>(listener.OnEvent);

// Publish
var event = new MyEvent("Hello World");
EventBus.Publish(event);

// Unsubscribe when done
EventBus.Unsubscribe<MyEvent>(listener.OnEvent);
```

### Using IEventMultiListener

If you want to listen to multiple events, you can use the `IEventMultiListener` interface:

```csharp
public class MyMultiListener : IEventMultiListener, 
    IEventListener<Event1>, 
    IEventListener<Event2> {
    
    public void OnEvent(Event1 evt) {
        // Handle Event1
    }
    
    public void OnEvent(Event2 evt) {
        // Handle Event2
    }
}

// Subscribe to all events at once
var listener = new MyMultiListener();
listener.SubscribeToAll();

// Unsubscribe from all events at once
listener.UnsubscribeFromAll();
```

### Example

Check out the [Examples](Runtime/Examples) folder for complete examples of how to use the EventFramework in your Unity projects.

## API Reference

### Core Interfaces

- `IEvent` - Base interface for all events
- `IEventListener<T>` - Interface for listening to specific events
- `IEventMultiListener` - Interface for listening to multiple events

### EventBus

- `Subscribe<T>(Action<T> listener)` - Subscribe to an event
- `Unsubscribe<T>(Action<T> listener)` - Unsubscribe from an event
- `Publish<T>(T evt)` - Publish an event
- `ClearAll()` - Clear all subscriptions

### Extensions

- `SubscribeToAll(this IEventMultiListener listener)` - Subscribe to all events for a multi-listener
- `UnsubscribeFromAll(this IEventMultiListener listener)` - Unsubscribe from all events for a multi-listener

## Best Practices

1. **Keep events immutable**: Events should be immutable to prevent unexpected behavior
2. **Unsubscribe when done**: Always unsubscribe from events when you're done with them to prevent memory leaks
3. **Use meaningful event names**: Name your events clearly to indicate what they represent
4. **Keep event data minimal**: Only include the data that's necessary for the event
5. **Handle exceptions**: The EventBus will catch exceptions in listeners, but it's good practice to handle them yourself

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Credits

Created by [AdaDeSions](https://github.com/adadesions) at [AdaBrain Studio](https://adabrain.studio). 