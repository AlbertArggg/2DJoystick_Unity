# 2DJoystick_Unity - Customizable Joystick asset for touch screen input

Joystick - PropollyGDS
- How to use:
   1) Import Package into Project.
   2) Drag and drop player prefab into scene.
   3) Enjoy!
   
- Customize Player:
   - Speed: the speed variable in the player object refers to the player's speed.
     this will allow the player to move across the playable area faster / slower 
     based on the value in Speed.
     
   - Max Rotation Angle: the max rotation angle is only active if directional rotation x 
     is true and directional rotation y is false.
     while moving left, the player will rotate up to -(Max Rotation Angle).
     while moving right, the player will rotate up to +(Max Rotation Angle).
     Return Rotation Speed: if, return to original rotation is true, the player's 
     character will rotate back to (0,0,0) once the joystick is no longer receiving 
     input Return rotation speed is the speed at which the player will rotate to 
     (0,0,0).
     
   - Return to Original Rotation: If this variable is set to true, then the player will 
     rotate back to (0,0,0) once the joystick is no longer receiving input. if set 
     to false, the player will maintain its rotation.
     
   - Freeze Pos X: if true, player will only be able to move up and down.
   
   - Freeze Pos Y: if true, player will only be able to move left and right.
   
   - Directional Rotation X: if true, player will rotate left and right based on input.
   
   - Directional Rotation Y: if both Directional Rotation X and Directional Rotation Y 
     are true, player will be able to rotate 360 degrees based on joystick input.
     
   - Customize Joystick: 
     Drag Threshold: the minimum drag distance required to register joystick input.
     
     Drag Movement Distance: the maximum distance that the joystick can be moved from its center.
     Drag Offset Distance: the maximum distance that the joystick can be dragged from its initial position
