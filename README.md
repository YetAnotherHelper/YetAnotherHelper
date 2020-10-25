# Yet Another Helper
Yet Another Helper for Celeste, what were you expecting?

The mod can be downloaded on [GameBanana](https://gamebanana.com/gamefiles/13148).

This mod is a list of entities and triggers I've thought or wanted to create because of a map or suggestions from others. If you want to submit feedback, an idea or ask for support, please mention me (Norah#1029) on [Celeste Discord Server](https://discord.gg/celeste) or submit an issue to this repository.

## For Mappers
If you are using this mod for your map, don't forget to list it as a dependency in `everest.yaml` like in the following example:
```yaml
- Name: YetAnotherHelperTestMap
  Version: 1.0.0
  Dependencies:
    - Name: YetAnotherHelper
      Version: 1.0.0
```

## Entities and Triggers
An updated list of entities and triggers with their respective explanation of what they do is on [GameBanana](https://gamebanana.com/gamefiles/13148) but can also be found here:

### Entities
- **Flag Kill Barrier**: A barrier that will kill you if you have the flag assigned to it.

### Triggers
- **Flag Kill Box**: Same as Flag Kill Barrier but invisible.
- **Lightning Strike Trigger**: Creates a Lightning Strike on the background according to settings given by the mapper.
- **Spawn Jelly Trigger**: Spawns a jelly where the player currently is, can be set to be multiple times.
