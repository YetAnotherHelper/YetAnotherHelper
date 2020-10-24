module YetAnotherHelperLightningStrikeTrigger

using ..Ahorn, Maple

@mapdef Trigger "YetAnotherHelper/LightningStrikeTrigger" LightningStrikeTrigger(x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight, playerOffset::Number=0.0, verticalOffset::Number=0.0, strikeHeight::Number=0.0, seed::Integer=0, delay::Number=0.0, rain::Bool=true, flash::Bool=true, constant::Bool=false)

const placements = Ahorn.PlacementDict(
    "Lightning Strike (Yet Another Helper)" => Ahorn.EntityPlacement(
        LightningStrikeTrigger,
        "rectangle",
    ),
)

end