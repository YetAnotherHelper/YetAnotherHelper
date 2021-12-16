local lightningStrikeTrigger = {}

lightningStrikeTrigger.name        = "YetAnotherHelper/LightningStrikeTrigger"
lightningStrikeTrigger.placements  = {
    name = "normal",
    data = {
        playerOffset    = 0.0,
        verticalOffset  = 0.0,
        strikeHeight    = 0.0,
        seed            = 0,
        delay           = 0.0,
        raining         = true,
        flash           = true,
        constant        = false
    }
}

return lightningStrikeTrigger
