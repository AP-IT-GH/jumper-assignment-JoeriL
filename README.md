# jumper-assignment-Joeri Lommers
jumper-assignment-JoeriL created by GitHub Classroom

# making level
## in the hierarchy
1. create an empty object called "trainingarea"
2. in this object create 4 cubes(floor, agent, Target-avoid and target-hit)<br />
![image](https://user-images.githubusercontent.com/65231997/233496155-92d9772b-50d2-4714-8d4b-336dc43a8249.png)

## in the level window
1. edit the scaling of the floor so it becomes flat and long<br />
![image](https://user-images.githubusercontent.com/65231997/233496041-5bb234c1-4477-43f5-a76c-ea6ebeab894d.png)


2. place the agent cube on top of the floor and the 2 target cubes underneath it(you can add materials for color)<br />
![image](https://user-images.githubusercontent.com/65231997/233496512-5a3267fd-aaee-4380-9ff5-54070924056a.png)

3. For the agent add the following components: Rigidbody(with the right constraints) and the Cube agent script and add the right objects(see picture below).
from the ML agents package add the scripts: Decision requester and behavior parameters. by behavior parameters, you can change the trained model if you have trained one yourself. 

![image](https://user-images.githubusercontent.com/65231997/233496996-b561f031-859c-47fb-a271-d6b94a296a50.png)<br />

4. In both the target object you need to add a Rigidbody with the right constraints (see picture below) and the move script. add the right object into the move target field and change the movementmultiplier to something you want.
![image](https://user-images.githubusercontent.com/65231997/233498096-82ddb4eb-4fc9-4a47-bcef-8c6993d47e03.png)

#
## scripts
CubeAgent.cs<br />
*for more clarity, open the script along with this short explanation*<br />
<br />
1. make sure you have the right dependencies.
![image](https://user-images.githubusercontent.com/65231997/233500890-ead1b38a-1ea1-4145-a02f-e984f0ccb29f.png)

2. create the variables: multiple targets that need to be taken in consideration, a boolean to let the move script know to move the target cubes and a speedMultiplier to change the jumpspeed of the agent.

3. Override the class OnEpisodeBegin(). Here you will add checks for the start of every episode like checking if the position of the agent is correct. You will also need to add the random value that determines wich target cube will be placed on the floor above and set the move boolean to true.

4. Override the class OnActionRecieved(). here you willl need to add float values of the distances between the agent and the targets as well as the floor. You will also need to add a vector3 controlsignal from the actionbuffers and map this to the y value of the controlesignal. Then check if the agent is on the ground and if this is true use the controlesignal to add a vertical force to the agent. Change the forcemode to impulse.<br /> Add a reward system that first checks wich target cube is on the floor. Then check if the cube hits the agent or if the cube goes past the agent. Then you give a reward. you can choose these rewards yourself. Then set the mover boolean to false and reset the target cubes position to beneath the floor. finally you can generate a new random in the move class(wich i will explain after this) and end the episode.<br />
(optional): you can check if the agent is on the floor and give him a reward for standing on the floor as long as he can.<br />

move.cs<br />
*for more clarity, open the script along with this short explanation*<br />
<br />
1. Create an gameobject variable for the cube you will be moving. Make a move booleanknow if the cube should be moving. Also a movement multiplier will need to be created to determin the max speed of the cubes. Lastly we will create a random float for the variable movementspeed.

2. In the start() method we create a random value

3. In the update() method we check in the CubeAgent script if the cube should be moving and if it is true we move the cube in the direction of the agent while taking into consideration a min speed of 3 , the ;ultiplier and the random value.

4. We create a final static method of generating a new random that will be called from the CubeAgent script
<br />

CubeAgent.yaml<br />
Add a new folder in the topdirectory of the project named config. Here you will add the CubeAgent.yaml file
![image](https://user-images.githubusercontent.com/65231997/233503596-550601f0-fa6d-48e2-9939-600021b1bb4f.png)
<br />
## tensorboard
![image](https://user-images.githubusercontent.com/65231997/233494697-aab0b962-4b1b-4060-a6b0-317ef7a3b5d4.png)
<br />
the training of the agent started off pretty slow. 1/3 into the program it started to make al lot of progress. At the end of the program the progress slowed down a lot. If the program was longer the cube would have a more consistant reward at the end.
