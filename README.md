
# Imitating Windows 95 Pipes Screensaver in Unity


![Pipes](/Assets/Preview/pipes.gif)

This project is an imitation of the Windows 95 Pipes Screensaver in Unity 3D. 
The intention is to implement the basic functionality of the screensaver including:
- Random movement across x,y and y axis
- When a collision occurs, the pipe searches for an available direction.
  If there is one, the pipe rotates towards it, otherwise a new pipe is spawn in a random position in space
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

## Handling collisions and movement

The head of the pipe can collide with all objects that are not labeled as UI layer;
that is so, in order to achieve collisions only with the desired game objects. If the
head collides with a body part or with the borders, then it should rotate to another 
available direction. If there is no direction available, then it should stop its movement 
and a new pipe with a new random color is instantiated. The prediction if there is a spot
available for the pipe head to rotate / move is achieved with the Physics.Raycast function
which predicts collisions. 
