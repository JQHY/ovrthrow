# oVRthrow

Welcome to oVRthrow's repository! oVRthrow is a VR fitness game inspired by Atari's Rampart arcade game.

Prepared by Jonathan Yeap, Isaac Ray, Ben Belay and Chris Stuttard.

## Overview

The directory 'OVRTHROW Source Project' contains all the source code to run the project in Unity.

We have included a Unity build for running the game quickly. To use it, simply unzip OVRTHROW.zip, and run the OVRTHROW.exe application. For more information, see the "Quick Start Guide" in "Ovrthrow User Manual.pdf".

## Setup & Requirements

oVRthrow is built for SteamVR, and was developed using the Quest 2 and Oculus Link. SteamVR also makes the game compatible with a large range of Virtual Reality headsets, and there is a full list of these in Section 2.2 of the user manual. Note that all of these headsets (with the exception of Quest 2 via AirLink) must be connected to the PC via a cable. To run this application on your PC, install the latest available version of SteamVR. Please see section 2.2 of the user manual for a step-by-step guide of setting up.

The Unity files were developed with Unity Editor version 2020.3.2. It is reccommended that the same editor version is used for running source code and making builds.

## Game Menu

When you first load the game, you will have to calibrate your height, and then be greeted by the main menu. Here you can change the game difficulty or watch the tutorial. Changing the difficulty of the game will result in enemy walls requiring more hits to destroy, and cannons firing more accurately. Hit play to run the game. 

## Gameplay

The game lasts five rounds.

Each round of the game consists of three phases; attack phase, repair phase and repair phase. The duration of these are 60 seconds, 30 seconds and 20 seconds. 

In the attack phase, you must throw cannonballs using a kettlebell swing to destroy the enemy castle. Cannonballs are picked up by holding the grip buttons on your VR controllers. Meanwhile, enemy cannons will be firing at your castle, and causing you damage.

The repair phase follows, where you must try to repair your castle walls. Damaged walls will be highlighted in green, and turning to face them will select them in blue. You must then hold your trigger buttons and make a squat, to repair each selected wall piece. Particle animations of bricks will appear to show you are making a repair!

The rest phase simply allows you to catch your breath, before another round starts.

After each round, the total health of the enemy walls increases, and so will take more throws to destroy.

The more damage you can do to the enemy, and the more walls you can repair, the more points you earn! However, you will also lose points if you don't repair your walls. Try to beat your highscore!

For more info about playing the game, and troubleshooting, check out the user manual. 

## Features in Development (Prototypes)

The game currently only has protoype Watson speech commands. We have implemented Watson SDK in OVRTHROWPLUS, and have included the additional files needed in "OVRTHROW With Voice Commands Source.zip". These additional files would need to be added to the 'OVRTHROW Source Project' files. To download OVRTHROWPLUS with the additional files already integrated, use this link: https://drive.google.com/drive/folders/1ocCH0k_O0EMIQ50_HTle7ievwJnpHDXM?usp=sharing

To quickly run OVRTHROWPLUS, simply unzip "OVRTHROWPLUS.zip" and run "OVRTHROW.exe". The OVRTHROWPLUS build is moderately less stable than classic OVRTHROW, so please keep this in mind. Please refer to the "Quick Start Guide" in the user manual for more setup information. 

Please note that all the OVRTHROWPLUS files are password protected as they contain private Watson keys. This password is with the client. You can find more information on the Watson features of the game in Section 3.5 of the user manual.

We have prototyped a swing analyser that checks your form when making kettle bell swing. This is currently prototyped for desktop, before being implemented in VR. This feature would check that your making an accurate swing, and then show a text prompt of "Good Swing!". Making a bad swing will show a text prompt of "Bad Swing" and tell you what was wrong. The Unity source files are found in Prototypes/Swing_Analyser. Step-by-step instructions are included in the game scene. 

There is yet to be music and sound effects in the game.

## Our Client

This project was commissioned by IBM, and IBM Master Inventor John McNamara. We have really enjoyed collaborating!

## Contact

For any questions with using the project, please email: isaac.r.ray@durham.ac.uk









