module YetAnotherHelperSpikeJumpThruController

using ..Ahorn, Maple

@mapdef Entity "YetAnotherHelper/SpikeJumpThruController" SpikeJumpThruController(x::Integer, y::Integer, persistent::Bool=false)

const placements = Ahorn.PlacementDict(
    "Spiked Jump Through Controller (Yet Another Helper)" => Ahorn.EntityPlacement(
        SpikeJumpThruController,
        "point"
    )
)

sprite = "ahorn/YetAnotherHelper/spikeJumpThruController"

function Ahorn.selection(entity::SpikeJumpThruController)
    x, y = Ahorn.position(entity)

    return Ahorn.getSpriteRectangle(sprite, x, y)
end

function Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::SpikeJumpThruController, room::Maple.Room)
	Ahorn.drawSprite(ctx, sprite, 0, -0)
end

end