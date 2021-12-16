local removeLightSourcesTrigger = {}

removeLightSourcesTrigger.name        = "YetAnotherHelper/RemoveLightSourcesTrigger"
removeLightSourcesTrigger.placements  = {
    name = "normal",
    data = {
        offsetTo        = 0.0,
        offsetFrom      = 0.0,
        positionMode    = "NoEffect",
        onlyOnce        = false
    }
}

return removeLightSourcesTrigger
