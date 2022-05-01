# GameEngines2_NatureSimulation
A nature simulation created for my Game Engines 2 module

## My Plan:
A nature simulation in which animals and the environment are generated when the game loads. I want to split the level generation up into biomes, where each biome has a few animals in it, each with their own tendencies. There will be predators and prey that need feeding, so the prey will feed on fauna and congregate in areas of interest to them (oasis' and forests where they can find plants to eat) and the predators will seek them out. Prey will flee when being chased by predators. 
The final submission will be a Planet Earth style video where you get a look at different types of animals and the camera will follow them around to see what they do during the simulation.

## Desired Features:
- A procedural terrain, with biomes for forest, planes and mountain.
- Particular animals who spawn and live in each terrain.
- Predator and Prey system.
- POIs for animals to congregatae around.

## Progress:
- Created the base project.
- Imported 3D models for a small selection of animals.
- Created a movement script using Seek, Arrive and Path Following.
- Created SteeringBehaviour and StateBehaviour classes.
- Implemented SteeringBehaviour based movement system for base Animal class.
- Implemented StateMachine for StateBehaviour on base Animal class.
- Created a series of steering and state behaviours that run between eachother.
- Created stats and food/water system for aniamls to survive.
- Attempted terrain generation, but resorted to using pre packaged models instead.
- Created hunting system for predators that hunt/kill their food.


## Planned Video Storyboard:
The video idea I have is a Planet Earth style documentary where there are scenes displaying animal interactions in different ways as if recorded for an episode of the show.  
### Sample Shots:
| **Sketch** | **Idea** |
| --- | --- |
| ![Bushesh](https://user-images.githubusercontent.com/55562147/155888180-005858a6-77f8-4fe7-8405-dca6f475dccf.jpg) | The first shot idea I have is as if you are in the bushes watching an interaction from a far. The interaction I've drawn is two deer at a lake drinking. |
| ![Helicopter](https://user-images.githubusercontent.com/55562147/155888213-b7098b1b-7d6e-4554-8b9d-63b7a657c61c.jpg) | The second shot idea I have is from a helicopter that is flying over one of the biomes. It looks down and you can see several different species wandering around and possible a predator/prey interaction also. |
| ![POV](https://user-images.githubusercontent.com/55562147/155888228-c800d242-e396-44b1-b809-63e2cd29acac.jpg) | The thirtd shot idea I have is from the POV of one of the animals. This doesn't massively fit the Planet Earth documentary idea but I think it would be an interesting shot. |

## Behaviours
![image](https://user-images.githubusercontent.com/55562147/165130155-436cc9bb-0e0f-4a20-96d4-595f467f7ee9.png)  
Above is a flow chart of each animal's State Machine and how they interacted with eachother. There are two main things in the game that change an animals state machine. The first is if they eat Animals or Resources available in the world. If they eat animals then they have a hunting state which makes them hunt for an animal smaller than them to kill and eat. The other is if the animal is a pack animal or not. If they like to be in packs then they have a loneliness stat that depletes over time and when it gets low enough they either create a new group or join an existing one for their species.  

## Code
I wanted to make it as easy as possible to create different types of animals, and the best way I could think of doing that was to create a basic Animal class that each animal would inherit from. I created the base animal class that held animal stats like movement speed, health and hunger as well as having code to move based on steering behaviours and code to manage the animals stats through runtime.

### Steering Behaviours
I didn't use many steering behaviours for these animals since they only traversed on two axes. The main ones that every animal used are seek and arrive, then animals that would travel in groups used offset pursue. These three were the only ones I needed to use since most of the code deviding on where the animals should go and how they should do it was based in their state machine behaviours. I used the weighted prioritised truncated running system that we did in class to manage what steering behaviours to use and had the state behaviours disable / enable specific ones if needed. E.g Offset Pursue was enabled in the FollowLeader behaviour.

### State Behaviours
I created a state machine on the [base animal class](https://github.com/evskii/GameEngines2_NatureSimulation/blob/main/NatureSimmulation/Assets/Scripts/AnimalStuff/Animals/Animal.cs) that would allow you to enter a new state and exit an old state so I could easily manage switching between states. I created a base [BehaviourState](https://github.com/evskii/GameEngines2_NatureSimulation/blob/main/NatureSimmulation/Assets/Scripts/Behaviours/BehaviourStates/BehaviourState.cs) class that each behaviour would inherit from. The base class had an Enter, Exit and Think method that would be called when needed to influence the animal correctly for each behaviour.

One of the bigger behaviours is the [LookForFood](https://github.com/evskii/GameEngines2_NatureSimulation/blob/main/NatureSimmulation/Assets/Scripts/Behaviours/BehaviourStates/Behaviour_LookForFood.cs) / [LookForWater](https://github.com/evskii/GameEngines2_NatureSimulation/blob/main/NatureSimmulation/Assets/Scripts/Behaviours/BehaviourStates/Behaviour_LookForWater.cs) behaviour. These are triggered when the animal is hungry / thirsty. In Enter() the animal finds all food sources in the scene that match the type of food they eat, then out of that list finds the closest to the animal. It sets this as the current target for the animal to go to and in Think() it checks distance to the food / water source. When within range it changes state to [Consuming](https://github.com/evskii/GameEngines2_NatureSimulation/blob/main/NatureSimmulation/Assets/Scripts/Behaviours/BehaviourStates/Behaviour_Consuming.cs) / [Drinking](https://github.com/evskii/GameEngines2_NatureSimulation/blob/main/NatureSimmulation/Assets/Scripts/Behaviours/BehaviourStates/Behaviour_Drinking.cs) and the animal regenerates their stats. 
    If the animal perefers to eat other animals, then it looks for animals that are smaller than it and targets them. It enters the [Hunting](https://github.com/evskii/GameEngines2_NatureSimulation/blob/main/NatureSimmulation/Assets/Scripts/Behaviours/BehaviourStates/Behaviour_Hunting.cs) state which gives them the ability to sprint, as long as they have stamina for it, towards the targetted animal and if they are within range then it attacks. Once it kills the animal then it enters the consuming state to regenerate its stats.  
    
Another larger behaviour is the [LookForGroup](https://github.com/evskii/GameEngines2_NatureSimulation/blob/main/NatureSimmulation/Assets/Scripts/Behaviours/BehaviourStates/Behaviour_LookingForGroup.cs) state. When the animal is lonely (and if they area a grouping animal) then they look for groups within their range. If there are any then it walks to the group and joins them using the [FollowLeader](https://github.com/evskii/GameEngines2_NatureSimulation/blob/main/NatureSimmulation/Assets/Scripts/Behaviours/BehaviourStates/Behaviour_FollowLeader.cs) behaviour. If there are no groups then it makes itself a group leader and other animals can join its group. It then just wanders and gradually other animals join and leave its group.

### Terrain Generation
I attempted a [terrain generation](https://github.com/evskii/GameEngines2_NatureSimulation/blob/main/NatureSimmulation/Assets/Scripts/TerrainGeneration/TerrainGenerator.cs) system based on what we had learned in class. I first generated a large plane with perlin noise but this proved too bumpy or too plane and there was no inbetween. I looked into seeded perlin noise but it was something that I would probably do if I had more time. I then changed it so I would make a bunch of smaller planes that slot together. I had to write into the generation code to have all of the borders at 0 so putting the smaller planes together would work better but this looked awful. I then added in a skirt that would lerp from 0 into the perlin noise accross a border (width set in the inspector) which started to look better. My FPS started to take a hit though and I went back to just using a flat plane and imported models since that had the best performance.

## Final Video


https://user-images.githubusercontent.com/55562147/166162102-4c8c57c6-a13f-4e33-a0a0-8a3495a5dc09.mp4





## References:
Wild Animals: https://free-game-assets.itch.io/free-wild-animal-3d-low-poly-models  
Environment Assets: https://www.kenney.nl/assets/nature-kit  
Skybox: https://assetstore.unity.com/packages/2d/textures-materials/sky/fantasy-skybox-free-18353  
