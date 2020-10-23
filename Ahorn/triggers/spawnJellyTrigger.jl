module YetAnotherHelperSpawnJellyTrigger

using ..Ahorn, Maple

@mapdef Trigger "YetAnotherHelper/SpawnJellyTrigger" SpawnJellyTrigger(x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight, onlyOnce::Bool=true)

const placements = Ahorn.PlacementDict(
    "Spawn Jelly (Yet Another Helper)" => Ahorn.EntityPlacement(
        SpawnJellyTrigger,
        "rectangle"
    )
)

end