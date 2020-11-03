module YetAnotherHelperStickyJellyfish

using ..Ahorn, Maple

@mapdef Entity "YetAnotherHelper/StickyJellyfish" StickyJellyfish(x::Integer, y::Integer, width::Integer=Maple.defaultBlockWidth, height::Integer=Maple.defaultBlockHeight)

const placements = Ahorn.PlacementDict(
    "Sticky Jellyfish (Yet Another Helper)" => Ahorn.EntityPlacement(
        StickyJellyfish,
        "point"
    )
)

sprite = "objects/glider/idle0"

function Ahorn.selection(entity::StickyJellyfish)
    x, y = Ahorn.position(entity)

    return Ahorn.getSpriteRectangle(sprite, x, y)
end

function Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::StickyJellyfish, room::Maple.Room)
    Ahorn.drawSprite(ctx, sprite, 0, 0)
end

end