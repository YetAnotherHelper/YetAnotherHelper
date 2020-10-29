module YetAnotherHelperRemoveLightSourcesTrigger

using ..Ahorn, Maple

@mapdef Trigger "YetAnotherHelper/RemoveLightSourcesTrigger" RemoveLightSourcesTrigger(x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight,
    offsetTo::Number=0.0, offsetFrom::Number=0.0, positionMode::String="NoEffect", onlyOnce::Bool=false)

const placements = Ahorn.PlacementDict(
    "Remove Light Sources (Yet Another Helper)" => Ahorn.EntityPlacement(
        RemoveLightSourcesTrigger,
        "rectangle",
    ),
)

function Ahorn.editingOptions(trigger::RemoveLightSourcesTrigger)
	return Dict{String, Any}(
		"positionMode" => Maple.trigger_position_modes
	)
end

end