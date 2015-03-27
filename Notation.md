# Introduction #

This document briefly explains the notation syntax used by the Rubik's Cube Bot parser for turns of the Rubik's Cube.

For more information see: http://www.speedsolving.com/wiki/index.php/Notation

Or you can visit the office WCA Cube Notation (not yet fully supported) here: http://www.worldcubeassociation.org/regulations/#notation

# Details #

  * Spaces are ignored and are irrelevant. Multiple spaces and tabs are treated as single spaces and are otherwise ignored. Carriage returns and newlines are also treated as white spaces and are ignored by the scanner.
  * Commands are turns or rotations of the cube as denoted [here](http://www.speedsolving.com/wiki/index.php/Notation).
  * Modifiers specify behavior of the preceding commands. Valid modifiers are the prime symbol **_'_**(example: **_F'_**) which denotes a counter-clockwise turn and integers which denote the number of time to make the given turn.
  * The same modifier can be used any number of times on a the same command. For example the command **_F''_** denotes a counter-counter-clockwise **_F_** turn or simply and _F_ turn.
  * Integers are all reduced to their mod 4 equivalents. For example the command **_U5_** is the same as **_U1_** or simply **_U_**