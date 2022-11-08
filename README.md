# Aalto University
# Coding Virtual Worlds - Assignment 3

This document aims at presenting the features implemented for the third assignment of Coding Virtual Worlds. The assignment consists in the creation of a simple Bowling VR game, as well as a simple scene that can be explored by moving around using the Oculus controllers.
This assignment was focused on learning the main functionalities provided by the XR Interaction Toolkit, including teleportation, continuous movement, snap movement, grab/release interactions and others.

More in detail, my implementation covers the following working functionalities:


- **Ball can be thrown and slips nicely** (0.5 point):<br>
The ball can be grabbed with any hand (right hand uses ray interactor, the left uses a direct interactor), and thrown by pressing and then releasing the grip button, while swinging the arm. The low drag and angular drag values allow the ball to roll for longer distances.

- **Pins fall and make proper sounds** (0.5 point):<br>
Both the bowling ball and the pins have colliders, and can collide with each other. Each pin uses the handler OnCollisionEnter to play a short impact sound, when hit.
Additionally, the fallen pins are destroyed after 2 seconds since the fall, using the script that is explained in the next point.

- **Scoring board counts** (0.5 point):<br>
The points are counted with the help of an invisible "detector" plane, that is located at approximately two thirds of the pins' height (touching the "neck" of the pins). This plane uses OnTriggerExit, which is activated when a pin falls down, thus exiting the plane's collider.
The handler then proceeds to increment the score by one for each fallen pin; the same handler is used to check how many pins are still standing, in order to implement the "Reset pins" feature (see below).<br><br>
Important note: since it was not further specified, the scoring system does not implement the rules of a real bowling game; the in-game score is simply the total number of fallen pins, and the 10 pins only reset after all the pins have fallen, as requested in "Reset pins".

- **Teleport and Snap Turn with Left Hand** (0.5 point):<br>
A GameObject called Locomotion System provides a Snap Turn Provider, that is bound to the left XR controller. The snap turn works exactly like in the demo apk, as no further changes were made.<br>
Similarly, the ground plane has a component Teleportation Area, that enables teleportation across the floor.

- **Continuous Movement and Turn with Right Hand** (0.5 point):<br>
The Locomotion System also provides Continuous Turn and Continuous Move Providers, that are bound to the right XR controller. Also here, the movement and turning works like in the demo apk.

- **Cannot move through walls, but can crouch underneath** (0.5 point):<br>
The XR Origin has a component called Character Controller, which works like a capsule collider; on the other hand, the scene includes a floating wall with a box collider, which has a gap that is low enough to not let the player through, unless the player crouches to pass beneath.

## Extra features

- **Add Background sounds, textures to objects** (0.5 points):<br>
A background sound was added, working in the same way as in Beat Saber. The sound effect used was the one provided in the Assignment page of the course website, along with the falling pin sound.<br>
To create the bowling ball and pins, instead of using primitives, I imported an asset pack from a third party. Credits are in the end of the document.<br>
To decorate the other elements of the bowling alley, a selection of third-party materials with wooden textures was also used.

- **Spawn new bowling ball in Socket when the ball is removed** (0.5 points):<br>
The table includes a child object with a component "XR Socket Interactor" attached. This functionality was added following the tutorial provided, and no further changes were made.

- **Add sounds to bowling ball** (1 point):<br>
The sound was imported from a third party, and it plays in loop when the bowling ball moves on the floor. The sound stops when the ball is close to reaching zero speed, or when it leaves the floor (e.g., if picked up, or fallen out).

- **Add text when all pins fall over and reset pins** (1 points):<br>
The XR Canvas includes an animator, that hides another text when in idle. The same script used for "Scoring board counts" has a counter of fallen pins, and if this counter reaches 10, the system will spawn 10 more pins after a 5 second timeout, and then reset the fallen counter (which is different from the cumulative score counter).<br>
During this 5 second timeout, the animator pops up a message "Restoring the pins, hold on...", and then hides it at the time of the respawn.

- **Add sound design when you move** (0.5 point):<br>
Sounds have been added in 3 types of movement: teleportation, snap turn, and continuous move.<br>
Teleportation sound was the easiest to add, since the "Teleportation Area" script allowed to add UnityEvents; an AudioSource.Play() was added on the "Teleporting" event slot.<br>
The other two cases involved inputs from the joysticks, which were not included in the provided CustomXRInput; as a consequence, CustomXRInput was edited to support this type of input, and other UnityEvents were used to play/stop the movement sounds.<br>
Teleportation and snap turn have two different "swoosh" sound effects, while the continuous movement is accompanied by a looping walking sound.

- **Add Custom Reticle in Teleportation Area and change appearance XR Ray Interactor** (0.5 points):<br>
The XR ray was changed to have a projectile shape, and its color was changed to a green/white gradient for interactable objects (valid ray), and it was kept as red/white when no interactions are available (invalid ray). A custom reticle is added to the valid ray at the collision point with the interactable object, and it consists in a simple particle effect that shows green/yellow gradient particles.

- **Add tunneling vignette to continuous movement for comfort** (0.5 point):<br>
A tunneling vignette was added for both continuous movement and continuous turn. The XR Origin's camera has a child object with the material and script to handle the fade-in/fade-out of the vignette when the player moves/stops.

- **Add a crouching mechanic, i.e., add a character controller in a way that the player has to crouch to reach a certain part of the world.** (0.5):<br>
As mentioned above, there is a floating wall that is low enough to not let the player through, except with crouch. The Character Controller adjusts the capsule collider's height dynamically according to the position of the head, while a Character Controller Driver fixes the minimum height to 0, so that the player always stays above the floor.


-------------------------------------------
Besides the requested features, I was able to add several models and animations imported from third parties. Below is a list of credits for the added features:

### Models:

Bowling ball with pins: https://www.turbosquid.com/3d-models/free-obj-mode-bowling-pins/1104623#

Animated VR Hands (fingers close in response to button presses): https://www.youtube.com/watch?v=DVAsL6sGmlc&list=WL&index=2

Wooden textures: https://assetstore.unity.com/packages/2d/textures-materials/wood/yughues-free-wooden-floor-materials-13213

Skybox: https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633


### Sounds:

Rolling bowling ball: https://www.findsounds.com/ISAPI/search.dll?keywords=bowling

Snap turn sound: https://samplefocus.com/samples/swoosh-fx-5798a2ff-2a08-4f85-a1a3-db0f3a9dcd74

Teleportation sound: https://samplefocus.com/samples/high-fast-swoosh

Footsteps: https://samplefocus.com/samples/cartoon-mixed-fx-footsteps
