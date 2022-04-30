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

## States
![image](https://user-images.githubusercontent.com/55562147/165130155-436cc9bb-0e0f-4a20-96d4-595f467f7ee9.png)

## References:
Wild Animals: https://free-game-assets.itch.io/free-wild-animal-3d-low-poly-models  
Environment Assets: https://www.kenney.nl/assets/nature-kit
Skybox: https://assetstore.unity.com/packages/2d/textures-materials/sky/fantasy-skybox-free-18353
