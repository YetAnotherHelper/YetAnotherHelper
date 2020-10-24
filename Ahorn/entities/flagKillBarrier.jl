module YetAnotherHelperFlagKillBarrier

using ..Ahorn, Maple

@mapdef Entity "YetAnotherHelper/FlagKillBarrier" FlagKillBarrier(x::Integer, y::Integer, width::Integer=Maple.defaultBlockWidth, height::Integer=Maple.defaultBlockHeight, falseColorHex::String="FFFFFF", trueColorHex::String="FF0000", flag::String="example_flag")

const placements = Ahorn.PlacementDict(
    "Flag Kill Barrier (Yet Another Helper)" => Ahorn.EntityPlacement(
        FlagKillBarrier,
        "rectangle"
    )
)

Ahorn.minimumSize(entity::FlagKillBarrier) = 8, 8
Ahorn.resizable(entity::FlagKillBarrier) = true, true

function Ahorn.selection(entity::FlagKillBarrier)
    x, y = Ahorn.position(entity)

    width = Int(get(entity.data, "width", 8))
    height = Int(get(entity.data, "height", 8))

    return Ahorn.Rectangle(x, y, width, height)
end

function Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::FlagKillBarrier, room::Maple.Room)
    width = Int(get(entity.data, "width", 32))
    height = Int(get(entity.data, "height", 32))
    
    Ahorn.drawRectangle(ctx, 0, 0, width, height, (0.45, 0.45, 0.45, 0.8), (0.0, 0.0, 0.0, 0.0))
end

end