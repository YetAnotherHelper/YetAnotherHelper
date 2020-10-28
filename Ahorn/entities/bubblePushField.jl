module YetAnotherHelperBubblePushField

using ..Ahorn, Maple

@mapdef Entity "YetAnotherHelper/BubbleField" YANBubbleField(x::Integer, y::Integer, width::Integer=Maple.defaultBlockWidth, height::Integer=Maple.defaultBlockHeight,
	strength::Number=1.0, direction::String="Right", flag::String="bubble_push_field", activationMode::String="Always")

const placements = Ahorn.PlacementDict(
	"Bubble Column (Yet Another Helper)" => Ahorn.EntityPlacement(
		YANBubbleField,
		"rectangle"
	)
)

Ahorn.editingOptions(entity::YANBubbleField) = Dict{String,Any}(
	"direction" => ["Up", "Down", "Left", "Right"],
	"activationMode" => ["Always", "OnlyWhenFlagActive", "OnlyWhenFlagInactive"]
)

Ahorn.minimumSize(entity::YANBubbleField) = 8, 8
Ahorn.resizable(entity::YANBubbleField) = true, true

Ahorn.selection(entity::YANBubbleField) = Ahorn.getEntityRectangle(entity)

function Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::YANBubbleField, room::Maple.Room)
	x = Int(get(entity.data, "x", 0))
	y = Int(get(entity.data, "y", 0))

	width = Int(get(entity.data, "width", 32))
	height = Int(get(entity.data, "height", 32))

	Ahorn.drawRectangle(ctx, 0, 0, width, height, (0.0, 0.0, 1.0, 0.4), (1.0, 1.0, 1.0, 0.5))
end

end