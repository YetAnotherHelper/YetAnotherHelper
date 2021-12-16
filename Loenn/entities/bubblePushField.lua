local bubblePushField = {}

bubblePushField.name        = "YetAnotherHelper/BubbleField"
bubblePushField.depth       = 0
bubblePushField.placements  = {
    name = "normal",
    data = {
        strength        = 1.0,
        direction       = "Right",
        flag            = "bubble_push_field",
        activationMode  = "Always",
        liftOffOfGround = true,

        width           = 8,
        height          = 8
    }
}

bubblePushField.fillColor   = { 0.0, 0.0, 1.0, 0.4 }
bubblePushField.borderColor = { 1.0, 1.0, 1.0, 0.5 }

return bubblePushField
