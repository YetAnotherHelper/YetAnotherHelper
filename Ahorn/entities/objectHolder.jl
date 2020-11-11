module YetAnotherHelperObjectHolder

using ..Ahorn, Maple

@mapdef Entity "YetAnotherHelper/ObjectHolder" ObjectHolder(x::Integer, y::Integer, spriteName::String="objects/YetAnotherHelper/objectHolder")

const placements = Ahorn.PlacementDict(
    "Object Holder (Yet Another Helper)" => Ahorn.EntityPlacement(
        ObjectHolder,
        "point"
    )
)

function Ahorn.selection(entity::ObjectHolder)
    x, y = Ahorn.position(entity)
	return Ahorn.Rectangle(x, y, 16, 8)
end

function Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::ObjectHolder, room::Maple.Room)
    Ahorn.drawImage(ctx, get(entity, "spriteName", "objects/YetAnotherHelper/objectHolder"), 0, 0)
end

end