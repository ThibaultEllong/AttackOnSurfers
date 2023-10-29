# AttackOnSurfers
A recreation of the Subway Surfer mobile game in the world of Attack on Titans using Unity3D

You can find a video demo here: https://www.youtube.com/watch?v=gg_-t6PEFp8&ab_channel=ThibaultEllong

As part of my Augmented & Virtual Reality master's diploma, I had to develop a video game using the game engine Unity, from scratch.
Without any prior knowledge and as a mostly autonomous project, I had to learn by myself most of the advanced scripting and game mechanic techniques used by Unity in order to create a compelling game.

In this blog post, I will go over the creation process of my game: Attack On Surfers

Check out a demo gameplay [![Watch the video](https://i.stack.imgur.com/Vp2cE.png)](https://youtu.be/gg_-t6PEFp8).

All the code of the game can be read and downloaded [here](https://github.com/ThibaultEllong/AttackOnSurfers/tree/main).

---

#### Inception

For some proper introduction, I'll start by introducing Attack On Titan:

Attack on Titan is a manga and anime created by Hajime Isayama in 2009. It is set in a medieval city surrounded by gigantic walls. Indeed, the outside of the city is flooded with titans: tall, naked, humanoid creatures whose only endeavour is to eat humans.
The story follows a young soldier called Eren Yaeger that mysteriously obtains the power to turn himself into a titan.
It is a very mature and dark story addressing topics such as death, revenge, remembrance and the unending cycle of hate and violence.

As a huge fan of the Attack On Titan series, I knew right from the start that I wanted to set my game in this universe. I think that it's a visually and thematically interesting world that I've spent a lot of time immersing myself in while watching the anime.

However, I had to come up with an idea for the gameplay. 

I first wanted to recreate the Omni-Dimensional Movement sequences that are so emblematic to the show. The characters are equipped with gas tanks, grappling hooks and long swords that they use to move in any direction and kill Titans.

With one month ahead of myself and no knowledge, I quickly understood that I was getting far ahead of myself and that it would surely lead to a half-finished, buggy game; or even to **nothing at all.**

Thus, I pivoted to a more manageable gameplay that would still be related to the anime.

I thought of an iconic scene in Episode 1, in which the newly formed soldiers take to the streets for their first fight against titans. They dash throught the corridors of this German-inspired city, running on the roofs and flying past the windows.

Using this scene as inspiration, it reminded me of a game I used to play as a teen: Subway Surfer.

What if the player, instead of dodging oncoming trains and running on top of wagons, sprinted on the roofing tiles and avoided titans?

After a few research, I found interesting tutorials and clones of the famous game and it appeared to me that this idea could be done in the given time.

Thus I went to work.

#### Creating the game

I wont go over all the technical details and the whole creation process as I didn't document it all.
However, I'll describe the main mechanics, their implementation and the challenges I faced and solved.

The game had to have 4 main components:

- A running player followed by a camera
- A random generated environment
- Obstacles and collectibles
- An appealing gameplay

##### Running Player

To create my character, I went on Adobe's ![Mixamo website](https://www.mixamo.com/#/). This free library provides animations and 3D models as well as an automatic rigging tool.
I selected this archer's model as it fitted the aesthetic I was going for:
[Image archer](/public/game_unity/archer.png)
I didn't think too much about its appearance as I just needed it for the programming phase.

I then wrote a script for the basic motion (jumping, running, sliding, changing lane) and animated it accordingly.

##### Random Environments

Subway Surfer uses randomly generated environments to create its levels ([about the difference between random and procedural environments](https://www.gamedeveloper.com/design/procedural-vs-randomly-generated-content-in-game-design)). This technique allows for infinite replayability as every restart is a new level.

I wanted to recreate this endless runner structure.

I first proceeded by creating 4 sections: 100 meters long corridors that would be placed sequentially in the world to create a corridor. Optimally, one can create tens of different sections to make the game seem procedurally generated.

I tried to have a particular obstacle type for each section to vary the gameplay and make it less repetitive.

---

##### Ostacles and collectibles

The first obvious obstacle was the titans. They are the first antagonist of the show and seemed easy to implement.
I downloaded a 3D model of a titan, animated it and placed it in the world (unfortunately, the animation didn't work and I was running out of time to fix it).

I also imported a medieval cart model over which you can jump or under which you can slide.

Finally, the rest of the obstacles are the buildings themselves. The player must be careful to calculate properly the length of his jumps and switch lanes at the proper moment not to run into a wall.

Moreover, if the player switches lane at the incorrect time and hits a side wall, he loses hp. When the health reaches 0, he dies.

##### An appealing gameplay

I tried to create a compelling experience by creating a face paced game, with good graphics and snappy movement.

I think this last part is the one I nailed the best.

You can jump, slide, switch lane and the lan you're in conditions where you can go next (left to right when jumping).

It's your turn to play and give me feedback about it !

---

Thank you for reading this short post.

My first experience at creating a videogame was quite interesting and very formative.
I like challenges and be ambitious when doing something new.

The game isn't perfect and a bit buggy on certain collisions but I'm happy about how it turned out!


---

#### Bibliography

- [Making a Subway SurferS game on Unity3D by Unity City](https://www.youtube.com/watch?v=4iMvBkaG-Jw&ab_channel=UnityCity)
- [Making an Endless Runner by Jimmy Vegas](https://www.youtube.com/watch?v=u5hRtTEhnOA&list=PLZ1b66Z1KFKit4cSry_LWBisrSbVkEF4t&ab_channel=JimmyVegas)
