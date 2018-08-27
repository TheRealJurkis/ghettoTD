# ghettoTD
##### quick links

[How to playe](#howto)
[Controls](#controls)
[About the code](#about)


### What does ghettoTD do?
This game was created as a part of a Uni programming course, with the main goal of learning how to put together a piece of software
with reasonable object-oriented design.

### How to start the game:
The game should be launched by double clicking on ghettoTDLauncher.exe in the main folder.
Then you have 5 seconds until enemies start swarming.


<a name="howto"></a>
### How to "play":
It is a simple sandbox-ish Tower Defense type game where you have enemies swarming towards a certain location following the fastest path.
Your goal is to build Towers in a way that creates enough time for the towers to shoot the enemies down.
You are awarded money for each kill which you can buy towers with.

Inherent part of the game is the inability to sell a tower or block-off (try it, they just pass through) enemies from reaching the finish tile.
It's DEFINETELY not an unfinished functionality that might be added later but a FEATURE.



<a name="controls"></a>
#### Controls

x                 : builds a tower


Right mouse click : selects a tile where the tower will be built


Space : Pause Game


Amount of money available are printed to console...


#gamescreen
![alt text](https://github.com/TheRealJurkis/ghettoTD/blob/master/objTD/Assets/Examples/example1.PNG "example")

<a name="about"></a>
#### About the code

The design is experimental and a mix of everything i read during the summer, therefore it has many flaws and inneficiencies which i'm either lazy to fix or not seeing yet.

#### Graphics:
Are done using [CSFML](https://www.sfml-dev.org/).

##### Something about the the code



The gameloop is in Core.cs, it is implemented as a fixed-time step loop to prevent the complexity added interpolation by implementing variable time step and freeing the physics from rendering. Which is something i definetely want to look into and try to implement.

##### Main class Game:


This class was an attempt to decouple components of a game, like player,towers, enemies and pathing algorithm.


##### PathFinder class:
The algorithm is inspired by : [GameAIPro: Crowd Pathfinding and Steering Using Flow Field Tiles](http://www.gameaipro.com/GameAIPro/GameAIPro_Chapter23_Crowd_Pathfinding_and_Steering_Using_Flow_Field_Tiles.pdf)


It's main benefit is that it saves cpu time in swarm-ish wave system where enemies spawn on random locations and therefore have different paths to finish.


The algorithm recalculates the shortest path to finish every time a tower is built, this changes a property of a TileNode called flow which is just a vector representing which direction should the enemy on it take to reach the finish.


   


