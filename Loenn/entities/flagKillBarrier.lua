local flagKillBarrier = {}

flagKillBarrier.name        = "YetAnotherHelper/FlagKillBarrier"
flagKillBarrier.depth       = 0
flagKillBarrier.placements  = {
    name = "normal",
    data = {
        falseColorHex   = "FFFFFF",
        trueColorHex    = "FF0000",
        flag            = "example_flag",

        width           = 8,
        height          = 8
    }
}

flagKillBarrier.fillColor   = { 0.79, 0.38, 0.38, 0.6 }
flagKillBarrier.borderColor = { 0.79, 0.32, 0.30, 0.7 }

return flagKillBarrier
