# Turn_Based_Strategy

## Overview
This repo contains a working prototype for a turn based strategy game in unity. 

## Controls

### Camera Controls
The player has full control over the camera through the use of the mouse or keyboard.
<img src="Readme/camera_controls.gif" width = "600">

### Movement
Movement range of units are determined through BFS and are displayed in red on the board when it is the unit's turn. A tile can then be selected to move to and the path to the tile is calculated using A*. The number of tiles the unit can move is primarily determined by the class of the unit (can be assigned in the unity inspector).

<img src="Readme/movement.gif" width = "600">
Note: First unit is a mage and second unit is a thief.

### Combat
Units can attack in a range determined by the weapon they have equipped. Upon engaging in combat camera controls are disabled and a combat camera is engaged. 

**Unit Stats**
Several systems are in place for damage calculation. (Look at DamageCalculator.cs for full formulas for damage calculation)
     Crit: Mutiplier is always 1.5x, gained from weapons/armor
     Dodge: Gained from armor and dex (has diminishing returns)
     Defense: Gained from armor and str
     Magic Defense: Gained from armor and int

### Abilities

## Other
Units on the team and actions of the currently selected unit can be cycled through using the buttons on screen.
<img src="Readme/other.gif" width = "600">



## AI
Note: The direction the AI unit faces is not fully implemented and the unit will face the direction it normally would after movement.

## Save/Load
