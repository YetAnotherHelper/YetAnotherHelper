module YetAnotherHelperStickyJellyfish

using ..Ahorn, Maple

@mapdef Entity "YetAnotherHelper/StickyJellyfish" StickyJellyfish(x::Integer, y::Integer)

const placements = Ahorn.PlacementDict(
    "Sticky Jellyfish (Yet Another Helper)" => Ahorn.EntityPlacement(
        StickyJellyfish,
        "point"
    )
)

sprite = "YAN/stickyJellyfish"

function Ahorn.selection(entity::StickyJellyfish)
    x, y = Ahorn.position(entity)

    return Ahorn.getSpriteRectangle(sprite, x, y - 5)
end

function Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::StickyJellyfish, room::Maple.Room)
    Ahorn.drawSprite(ctx, sprite, 0, -5)
end

end