using System.Windows.Forms;
using System.Drawing;
using NativeUI;
using GTA;
using GTA.Native;
using System;

public static class EntryPoint
{

    public static UIMenu scoreMain; // Creating the base menu

    private static UIMenuListItem setAssault; // Creating the Assault list
    private static UIMenuListItem setDoomsday; // Creating the Doomsday list
    private static UIMenuListItem setSmuggler; // Creating the Smuggler list

    private static UIMenuListItem scoreSet; // Creating the Play Score list
    private static UIMenuListItem scoreInt;// Creating the Score Intensity list
    private static UIMenuListItem playSound; // Creating the Play Sound submenu
    private static UIMenuCheckboxItem muteSounds; // Creating the Disable Sound checkbox
    private static UIMenuCheckboxItem muteRadio; // Creating the Disable Sound checkbox
    private static UIMenuCheckboxItem muteFlying; // Creating the Disable Flying Music checkbox
    private static UIMenuCheckboxItem muteWanted; // Creating the Disable Wanted Music checkbox

    private static MenuPool scorePool; // Creating the menu pool

    static void Main()
    {
        scorePool = new MenuPool();
        scoreMain = new UIMenu("Score Controller", "SCORE CONTROLLER"); // TO CHANGE TO BANNER!
        InstructionalButton stopScore = new InstructionalButton(GameControl.Jump, "Stop Score");
        InstructionalButton stopFrontendSound = new InstructionalButton(GameControl.FrontendSelect, "Stop Frontend Sound");

        scorePool.Add(scoreMain);

        scoreMain.AddItem(scoreSet = new UIMenuListItem("Select Score Set", "Select a Score set you want to listen a track from.", Arrays.scoreSets)); // Adding the Score Set list

        scoreMain.AddItem(setAssault = new UIMenuListItem("Select Track", "Select a Score track from SSA SSS.", Arrays.tracksAssault));
        setDoomsday = new UIMenuListItem("Select Track", "Select a Score track from the Doomsday Heist.", Arrays.tracksDoomsday);
        setSmuggler = new UIMenuListItem("Select Track", "Select a Score track from Smuggler's Run.", Arrays.tracksSmuggler);

        scoreMain.AddItem(scoreInt = new UIMenuListItem("Select Intensity", "None of the Score Tracks is playing.", Arrays.intNames)); // Adding the Intensity list
        scoreMain.AddItem(playSound = new UIMenuListItem("Select Sound", "Select a Frontend Sound to play.", Arrays.soundNames)); // Adding the Score Intensity list

        scoreMain.AddItem(muteSounds = new UIMenuCheckboxItem("Disable Sound", false, "Disable or enable all sound except Score/Frontend.")); // Adding the Disable Sound checkbox
        scoreMain.AddItem(muteRadio = new UIMenuCheckboxItem("Toggle MPData", false, "Disable or enable Radio.")); // Adding the Disable Radio checkbox
        scoreMain.AddItem(muteFlying = new UIMenuCheckboxItem("Disable Flying Music", false, "Disable or enable Flying Music.")); // Adding the Disable Flying Music checkbox
        scoreMain.AddItem(muteWanted = new UIMenuCheckboxItem("Disable Wanted Music", false, "Disable or enable Wanted Music.")); // Adding the Disable Wanted Music checkbox

        scoreMain.AddInstructionalButton(stopScore); // Adding the Stop Score key
        scoreMain.AddInstructionalButton(stopFrontendSound); // Adding the Stop FS key

        scoreMain.RefreshIndex();

        scoreMain.OnItemSelect += OnItemSelect;
        scoreMain.OnListChange += OnListChange;
        scoreMain.OnCheckboxChange += OnCheckboxChange;

        scoreMain.MouseControlsEnabled = true; // Mouse control
        scoreMain.AllowCameraMovement = false; // Camera movement
        scoreMain.MouseEdgeEnabled = true; // Mouse edge

        scoreInt.Enabled = false; // Disabling Score Intensity on startup
        scoreInt.ScrollingEnabled = false; // Disabling its scrolling

        //Sprite banner = new Sprite("shopui_title_scorecontroller", "shopui_title_scorecontroller", new Point(0, 0), new Size(0, 0)); // Banner
        //scoreMain.SetBannerType(banner);        

        MainLogic();
        GameFiber.Hibernate();
    }

    static void Buttons() // The function that enables/disables buttons
    {
        if (scoreSet.Enabled)
        {
            scoreInt.Enabled = true;
            scoreSet.Enabled = false;
            setAssault.Enabled = false;
            setDoomsday.Enabled = false;
            setSmuggler.Enabled = false;
        }
        else
        {
            scoreInt.Enabled = false;
            scoreSet.Enabled = true;
            setAssault.Enabled = true;
            setDoomsday.Enabled = true;
            setSmuggler.Enabled = true;

            scoreInt.Index = 0;
        }
    }

    public static void Description(UIMenuItem item) // Setting the descriptions
    {
        if (scoreSet.Enabled)
        {
            scoreSet.Description = Descriptions.item0_Descr;
        }
        else
        {
            scoreSet.Description = Descriptions.item0_DescrDisabled;
        }

        if (!scoreMain.MenuItems[1].Enabled)
        {
            scoreMain.MenuItems[1].Description = Descriptions.item1_DescrDisabled;
            scoreInt.Description = Descriptions.item2_Descr;
        }
        else
        {
            scoreMain.MenuItems[1].Description = Descriptions.item1_Descr;
            scoreInt.Description = Descriptions.item2_DescrDisabled;
        }
    }

    static void PlaySound(string name, string set) // Playing a Frontend Sound
    {
        NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, name, set, 0);
    }
    static void PlayScore(string name) // Playing a Score Track
    {
        NativeFunction.Natives.TRIGGER_MUSIC_EVENT(name);
        NativeFunction.Natives.PREPARE_MUSIC_EVENT("GTA_ONLINE_STOP_SCORE");
        NativeFunction.Natives.TRIGGER_MUSIC_EVENT("BTL_IDLE"); // Supporting Music Event so the track is playing
    }

    public static void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index) // Event on Play Sound X pressed
    {
        if (sender == scoreMain)
        {
            if (selectedItem == scoreSet) // Fancy thing to disallow selecting the Set
            {

                ScoreTimer.scoreTimer.Start();

                scoreSet.Selected = false;
                scoreMain.GoDown();
            }

            if (selectedItem == playSound)
            {
                string soundName = playSound.Collection[playSound.Index].Value.ToString();

                switch (soundName)
                {
                    case "Biker Tip":
                        PlaySound("Boss_Message_Orange", "GTAO_Biker_FM_Soundset"); // Playing Biker Tip
                        break;
                    case "CEO Tip":
                        PlaySound("Boss_Message_Orange", "GTAO_Boss_Goons_FM_Soundset"); // Playing CEO Tip
                        break;
                    case "Event About to Start":
                        PlaySound("Event_Start_Text", "GTAO_FM_Events_Soundset"); // Playing Event Start
                        break;
                    case "CEO Wage":
                        PlaySound("Goon_Paid_Large", "GTAO_Boss_Goons_FM_Soundset"); // Playing CEO Wage
                        break;
                    case "Biker Enemy":
                        PlaySound("Enemy_In_Zone", "DLC_Biker_SYG_Sounds"); // Playing Biker Enemy
                        break;
                }
            }

            if (selectedItem == setAssault)
            {
                string scoreName = setAssault.Collection[setAssault.Index].Value.ToString();

                switch (scoreName)
                {
                    case "Tech One":
                        PlayScore("RC_ASSAULT_1");
                        break;
                    case "Tech Two":
                        PlayScore("RC_ASSAULT_2");
                        break;
                    case "Tech Six":
                        PlayScore("RC_ASSAULT_3");
                        break;
                    case "Tech Seven":
                        PlayScore("RC_ASSAULT_4");
                        break;
                }

                Buttons();
            }

            if (selectedItem == setDoomsday)
            {
                string scoreName = setDoomsday.Collection[setDoomsday.Index].Value.ToString();

                switch (scoreName)
                {
                    case "CMH Heist One":
                        PlayScore("RC_HEIST2_1");
                        break;
                    case "CMH Heist Two":
                        PlayScore("RC_HEIST2_2");
                        break;
                    case "CMH Heist Three":
                        PlayScore("RC_HEIST2_3");
                        break;
                    case "Adv. Tech Six":
                        PlayScore("RC_HEIST2_4");
                        break;
                    case "Adv. Tech Seven":
                        PlayScore("RC_HEIST2_5");
                        break;
                    case "Adv. Tech Ten":
                        PlayScore("RC_HEIST2_6");
                        break;
                    case "Adv. Tech Eleven":
                        PlayScore("RC_HEIST2_7");
                        break;
                }

                Buttons();
            }

            if (selectedItem == setSmuggler)
            {
                string scoreName = setSmuggler.Collection[setSmuggler.Index].Value.ToString();

                switch (scoreName)
                {
                    case "Track Inferno":
                        PlayScore("RC_SMUGGLER_1");
                        break;
                    case "Track S One":
                        PlayScore("RC_SMUGGLER_2");
                        break;
                    case "Track S Two":
                        PlayScore("RC_SMUGGLER_3");
                        break;
                    case "Track S Three":
                        PlayScore("RC_SMUGGLER_4");
                        break;
                    case "Track S Four":
                        PlayScore("RC_SMUGGLER_5");
                        break;
                    case "Track SMTB One":
                        PlayScore("RC_SMUGGLER_6");
                        break;
                    case "Track SMTB Three":
                        PlayScore("RC_SMUGGLER_7");
                        break;
                    case "Track SMTB Four":
                        PlayScore("RC_SMUGGLER_8");
                        break;
                    case "Track SMTB Five":
                        PlayScore("RC_SMUGGLER_9");
                        break;
                }

                Buttons();
            }

            if (selectedItem == scoreInt)
            {
                string scoreIntensity = scoreInt.Collection[scoreInt.Index].Value.ToString();

                switch (scoreIntensity)
                {
                    case "Low":
                        Game.DisplayNotification("Low Intensity.");
                        NativeFunction.Natives.TRIGGER_MUSIC_EVENT("BKR_GUNRUN_DEAL");
                        break;
                    case "Mid":
                        Game.DisplayNotification("Mid Intensity.");
                        NativeFunction.Natives.TRIGGER_MUSIC_EVENT("BG_SIGHTSEER_MID");
                        break;
                    case "High":
                        Game.DisplayNotification("High Intensity.");
                        NativeFunction.Natives.TRIGGER_MUSIC_EVENT("DROPZONE_ACTION_HIGH");
                        break;
                }
            }
        }
    }

    public static void OnListChange(UIMenu sender, UIMenuListItem list, int index)
    {

        if (sender != scoreMain || list != scoreSet) return;


        switch (scoreSet.SelectedItem.ToString())
        {
            case "Assault":
                scoreMain.RemoveItemAt(1);
                scoreMain.AddItem(setAssault, 1);
                scoreMain.RefreshIndex();
                break;
            case "Doomsday":
                scoreMain.RemoveItemAt(1);
                scoreMain.AddItem(setDoomsday, 1);
                scoreMain.RefreshIndex();
                break;
            case "Smuggler":
                scoreMain.RemoveItemAt(1);
                scoreMain.AddItem(setSmuggler, 1);
                scoreMain.RefreshIndex();
                break;
        }
    }

    public static void OnCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked)
    {
        if (sender != scoreMain || checkbox == muteSounds) // Controlling Disable Sounds
        {
            switch (Checked)
            {
                case true:
                    Game.DisplayNotification("Sounds Muted.");
                    NativeFunction.Natives.START_AUDIO_SCENE("END_CREDITS_SCENE"); // Disabling non-Score-Frontend sound [ALSO MUTES FRONTEND!!!]
                    break;
                case false:
                    Game.DisplayNotification("Sounds Unmuted.");
                    NativeFunction.Natives.STOP_AUDIO_SCENE("END_CREDITS_SCENE"); // Enabling non-Score-Frontend sound
                    break;
            }
        }

        if (sender != scoreMain || checkbox == muteRadio) // Controlling Disable Radio
        {
            switch (Checked)
            {
                case true:
                    Game.DisplayNotification("MPData Enabled.");
                    NativeFunction.Natives.SET_AUDIO_FLAG("LoadMPData", 1);
                    break;
                case false:
                    Game.DisplayNotification("MPData Disabled.");
                    NativeFunction.Natives.SET_AUDIO_FLAG("LoadMPData", 0);
                    break;
            }
        }

        if (sender != scoreMain || checkbox == muteFlying) // Controlling Disable Flying Music
        {
            switch (Checked)
            {
                case true:
                    Game.DisplayNotification("Flying Music Disabled.");
                    NativeFunction.Natives.SET_AUDIO_FLAG("DisableFlightMusic", 1); // Enabling Flight Music
                    break;

                case false:
                    Game.DisplayNotification("Flying Music Enabled.");
                    NativeFunction.Natives.SET_AUDIO_FLAG("DisableFlightMusic", 0); // Enabling Flight Music
                    break;
            }
        }

        if (sender != scoreMain || checkbox == muteWanted) // Controlling Disable Wanted Music
        {
            switch (Checked)
            {
                case true:
                    Game.DisplayNotification("Wanted Music Disabled.");
                    NativeFunction.Natives.SET_AUDIO_FLAG("WantedMusicDisabled", 1); // Enabling Wanted Music
                    break;
                case false:
                    Game.DisplayNotification("Wanted Music Enabled.");
                    NativeFunction.Natives.SET_AUDIO_FLAG("WantedMusicDisabled", 0); // Enabling Wanted Music
                    break;
            }
        }
    }

    public static void MainLogic() // Key etc.
    {
        GameFiber.StartNew(delegate
        {
            while (true)
            {
                GameFiber.Yield();

                if (Game.IsControlJustPressed(246, GameControl.MpTextChatTeam) && Game.LocalPlayer.HasControl == true)
                {
                    scoreMain.Visible = !scoreMain.Visible;
                    scoreMain.RefreshIndex();
                    PlaySound("SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET"); // Playing open menu sound
                }

                scorePool.ProcessMenus();

                if (scoreMain.Visible) // Checks if the menu is visible to prevent an ability to stop Score/FS during normal gameplay
                {
                    NativeFunction.Natives.SET_AUDIO_FLAG("LoadMPData", 1); // Loading MPData

                    bool stopScorePressed = Game.IsControlJustPressed(22, GameControl.Jump);

                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(2, 22, true); // Disables the jump key

                    if (!scoreSet.Enabled && stopScorePressed)
                    {
                        if (stopScorePressed)
                        {
                            NativeFunction.Natives.TRIGGER_MUSIC_EVENT("GTA_ONLINE_STOP_SCORE"); // Stops Online Score

                            Buttons();
                        }
                    }

                    if (Game.IsControlJustPressed(217, GameControl.FrontendSelect))
                    {
                        Game.DisplayNotification("Frontend Sound Stopped.");
                        NativeFunction.Natives.RELEASE_SOUND_ID(-1);
                        NativeFunction.Natives.STOP_SOUND(-1);
                    }

                    for (int i = 6; i < 10; i++) // Hiding Vehicle/Area name and subtitles while the menu is active
                    {
                        NativeFunction.Natives.HideHudComponentThisFrame(i);
                    }
                    NativeFunction.Natives.HideHudComponentThisFrame(15);
                    NativeFunction.Natives.HideHelpTextThisFrame();
                }
                else
                {
                    NativeFunction.Natives.ENABLE_CONTROL_ACTION(2, 22, true); // Enables the jump key
                    //NativeFunction.Natives.SET_AUDIO_FLAG("LoadMPData", 0); // Disables MPData
                };

                Description(scoreInt);
                Description(scoreSet);
            };
        });
        Game.DisplayHelp("Welcome to the Score Controller! Press ~INPUT_MP_TEXT_CHAT_TEAM~ to open the Menu.", 5000);
    }
}