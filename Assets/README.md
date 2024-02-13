
# Imitating Windows 95 Pipes Screensaver in Unity

This project is an imitation of the Windows 95 Pipes Screensaver in Unity 3D. 
The intention is to implement the basic functionality of the screensaver including:
- Random movement across x,y and y axis
- When a collision occurs, a new pipe is spawn in a random position in space
- Each new pipe get a new random color



## The pipe object
The pipe object consist of a pipe prefab, a head prefab, a body prefab and a sphere prefab.
The **pipe prefab** is responsible for instantiating new pipes randomly in a limited space
The **head prefab** is a cylinder and it is responsible for the random 
movement of the pipe and carries the RandomMovement script.
The **body prefab** is a cylinder similar to the **head prefab** and it is instantiated
each time the **head prefab** moves its length in space. That ensures the smooth growing 
of the pipe. 
Finally the **sphere prefab** is a transparent sphere in front of the head prefab handling
all the collision. That way the visible pipe seems to handle collisions smoothly.

## Handling collisions

The collision handling is happening with the OnCollisionEnter method in the Collision script
and it can be applied to all game objects (including borders) that are not labeled as UI layer. 
That is so, in order to achieve collisions only with the desired game objects. 

#### TODOs
When a pipe is rotating in order to change direction, a corner object -sphere- is spawned.
However, in order to achieve a smoother corner representation, the method EndingPrefab in 
RandomMovement is instantiating a cylinder half the length of the **body prefab**. 
This method is still to be implemented, as it requires different handling in each axis
as the rotation of the **head prefab** changes in the 3D space.
