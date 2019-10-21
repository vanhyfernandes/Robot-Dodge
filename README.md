# Robot Dodge
 Robot game written in C# and using SplashKit

Rules:
- The player has 5 lifes;
- Score is counted based on the time that the player is alive;
- The player can move using the keys < >;
- The player can shoot using the key SPACE;
- The player can shoot to kill the robots;

Bullet Class
How are bullets modelled?
A bullet is created by the Shoot method on the player’s class. This class invokes the bullet constructor that creates a new bullet from an image that is on the image’s folder using SplashKitSDK.
The bullets are created based on the player’s location and angle on the window. 

How do bullets move?
The method Update is responsible for making the bullet move. It actualises the bullet location on the windows every time that is called.

How do bullets destroy the robots?
The method CollidedWith verifies if the bullet has collided with any robot by using the method CircleCollision from the bitmap and checking if it’s collided with the robot.
If the bullet collides with a Robot, the bullet and the Robot disappear from the window.

