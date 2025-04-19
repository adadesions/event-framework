# EventFramework Examples

This folder contains example code that demonstrates how to use the EventFramework in your Unity projects.

## Example Files

1. **PlayerEvents.cs** - Contains example event classes:
   - `PlayerSpawnedEvent` - A simple event with basic data
   - `PlayerHealthChangedEvent` - An event with more complex data
   - `PlayerStateChangedEvent` - An event that uses an enum

2. **PlayerEventListener.cs** - Contains example listener classes:
   - `PlayerSpawnListener` - A simple listener for a specific event
   - `PlayerHealthListener` - A listener that handles health changes
   - `PlayerStateListener` - A listener that uses the `IEventMultiListener` interface

3. **PlayerEventExample.cs** - A MonoBehaviour that demonstrates how to use the event system:
   - Shows how to subscribe and unsubscribe from events
   - Demonstrates publishing events
   - Includes example input handling to trigger events

## How to Use the Examples

1. Add the `PlayerEventExample` component to a GameObject in your scene.
2. Run the game and use the following keys to test the event system:
   - Press `H` to heal the player
   - Press `D` to damage the player
   - Press `1-5` to change the player state:
     - `1` = Idle
     - `2` = Walking
     - `3` = Running
     - `4` = Jumping
     - `5` = Falling

3. Check the console to see the event messages.

## Creating Your Own Events

To create your own events:

1. Create a class that implements the `IEvent` interface:
   ```csharp
   public class MyCustomEvent : IEvent {
       public string Data { get; }
       
       public MyCustomEvent(string data) {
           Data = data;
       }
   }
   ```

2. Create a listener that implements `IEventListener<MyCustomEvent>`:
   ```csharp
   public class MyCustomListener : IEventListener<MyCustomEvent> {
       public void OnEvent(MyCustomEvent evt) {
           Debug.Log($"Received event with data: {evt.Data}");
       }
   }
   ```

3. Subscribe to the event and publish it:
   ```csharp
   // Subscribe
   var listener = new MyCustomListener();
   EventBus.Subscribe<MyCustomEvent>(listener.OnEvent);
   
   // Publish
   var event = new MyCustomEvent("Hello World");
   EventBus.Publish(event);
   
   // Unsubscribe when done
   EventBus.Unsubscribe<MyCustomEvent>(listener.OnEvent);
   ```

## Using IEventMultiListener

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