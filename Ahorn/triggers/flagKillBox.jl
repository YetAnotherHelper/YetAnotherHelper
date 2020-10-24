module YetAnotherHelperFlagKillBox

using ..Ahorn, Maple

@mapdef Trigger "YetAnotherHelper/FlagKillBox" FlagKillBox(x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight, flag::String="example_flag")

const placements = Ahorn.PlacementDict(
    "Flag Kill Box (Yet Another Helper)" => Ahorn.EntityPlacement(
        FlagKillBox,
        "rectangle"
    )
)

end