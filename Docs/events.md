# Events

Events provide a lightweight way for systems and modules to communicate without tightly coupling logic together.
They are dispatched during a frame and can be listened to by any system that subscribes to that specific event type.

## Defining an Event

Events are simple structs
```cs
struct PlayerDiedEvent {
    string Name;
}
```

## Sending an Event

You can send events at any point during the frame using the following
```cs
world.Emit(new PlayerDiedEvent {
    Name = "player 1"
});
```

## Listening for Events

To listen for events, subscribe to the event with a function
```cs
[Subscribe<PlayerDiedEvent>]
void OnPlayerDeath(World world, PlayerDiedEvent ev) {
    Console.WriteLine($"Player {ev.Name} has died!");
}
```