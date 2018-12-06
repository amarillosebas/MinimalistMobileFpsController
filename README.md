# MinimalistMobileFpsController
Minimalist first person controller for mobile games with a joystick and one button.

Right half of the screen serves as one button, called trigger.
The left half area serves as a joystick. Wherever the player touches, a virtual joystick is created, and movement in any direction will create a vector that the camera controller recieves and converts into movement.

Camera and player movements are smoothed.

There's a reticle that aims at objects, and reacts when looking at an interactable object. When the trigger is pressed when looking at an interactable, it calls a method within the interactable. When the player isn't looking at anything interactable (the floor, or sky), it moves instead.

There are 5 "events" included: Trigger_Up, Trigger_Down, Trigger_Hold, Gaze_In, Gaze_Out.

I might not be the most elegant coder, but everything works fine.

I hope this is of use to someone.
