# ScoreController

Score Controller is a script for Grand Theft Auto V that allows player to control Interactive Music. Uses [NativeUI](https://github.com/Guad/NativeUI) as the menu base.

#### Components required (always install the latest stable versions available):
- ASI Loader and ScriptHookV: http://www.dev-c.com/gtav/scripthookv
- ScriptHookVDotNet: https://github.com/crosire/scripthookvdotnet/releases
- NativeUI: https://github.com/Guad/NativeUI/releases

#### Score Controller installation:
- Put ScoreController.dll in the "scripts" folder located in your Grand Theft Auto V installation directory.

#### How to use:
- During gameplay, bring up the menu by pressing the MP Team Chat button (Y by default) on your keyboard. It will not open if help text is displayed on screen, if you're using cellphone, or if you can't control the player at the moment (this prevents some bugs; might be changed in future);
- Select the Score Set you'd like to listen to a track from, and then select the track to listen to. It may take a few seconds for a track to start playing;
- Tracks start playing with the lowest intensity by default. You can control the intensity with the Intensity menu entry: select the intensity level you need and press the accept button (Enter by default);
- To play another track, stop the one that's currently playing. You can do this by pressing the key displayed in the right bottom corner of screen (Space by default);
- Mute radio or sound if necessary.

#### Known issues:
- If the Score Controller menu is triggered while the Interaction Menu is on screen, it will overlap the latter. Wasn't fixed yet, so I advice you not to bring up the Score Controller menu when the Interaction Menu is active;
- Score Controller can behave unexpectedly if you control Score during missions or other activities. I don't think it can cause something serious, probably some minor issues, but use carefully anyway;
- The Score Controller interface can also interfere with game's default UI elements. Again, nothing serious, but still an issue that hasn't been fixed yet;
- A lot of tracks are missing from the menu at the moment, though it's possible to add them. I will add missing tracks in future releases.

#### Licensing
You can use any part of this script's source code in your projects.
