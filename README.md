# Custom-Unity-Wwise-Scripts
Some custom Unity + Wwise scripts and extended components.
These components were initially written for a multiplayer shooter project.

They are highly reusable!

# CustomRoom.cs
Expands the AkRoom component, allowing designers to set an AK.Wwise.Switch to emitters inside of the room with only one trigger check, and within only one component.

Specifically useful for switching tails or layers of sounds based on the environment/room.

# CustomRoomInspector.cs
Expands AkRoomInspector, allows expanded properties to be displayed in the inspector.
Needs to be placed in a folder named 'Editor'.

# OneShotEnvironmentAwareEmitter.cs
A one stop shop to easily fire one shot sounds. Can be easily be spawned from anywhere with one line of code.
It bypasses the need for a prefab, and is automatically aware of AkEnvironments, AkRooms and Wwise Spatial Audio.

Automatically deletes itself when the event finishes for better performance.

In our project, it even served as a base class for other specific-use-case emitters, such as 'bullet impact emitters' and 'footstep emitters'.
