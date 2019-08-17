using System;
using GTA;
using GTA.Native;
using NativeUI;
using System.Windows.Forms;
using System.Drawing;

namespace Score_Controller
{
    public class Controller : Script
    {
        MenuPool controllerMenuPool;
        private static UIMenu controllerMain;

        private static UIMenuListItem mainScoreCollection;
        private static UIMenuListItem mainScoreTrack;
        private static UIMenuListItem mainScoreIntensity;
        private static UIMenuCheckboxItem mainMuteSound;
        private static UIMenuCheckboxItem mainMuteRadio;

        private static UIMenuItem mainCustomEvent;
        private static UIMenuItem mainCustomScene;

        private static bool IsScorePlaying = false; // The field to tell if a Track is playing
        private static bool IsSoundMuted = false; // The field to tell if sound is muted; ВОЗМОЖНО, НЕ ПРИГОДИТСЯ
        private static bool IsRadioMuted = false; // The field to tell if radio is muted

        private static ScoreTrack currentScoreTrack = null; // Currently selected Score Track
        private static string currentAudioScene = null; // Currently active Audio Scene

        private static Sprite bannerScoreController = new Sprite("shopui_title_scorecontroller", "shopui_title_scorecontroller", new Point(0, 0), new Size(0, 0)); // Creating the banner

        private static InstructionalButton buttonStopScore = new InstructionalButton(GTA.Control.Jump, "Stop Score"); // Creating the Stop Score button
        private static InstructionalButton buttonStopScene = new InstructionalButton(GTA.Control.SelectWeapon, "Stop Scene"); // Creating the Stop Scene button

        public Controller()
        {
            Tracks.AddTracks(); // Adding all tracks
            Collections.AddCollections(); // Adding all collections
            Intensities.AddIntensities(); // Adding all intensities

            controllerMenuPool = new MenuPool();
            controllerMain = new UIMenu(Text.controllerTitle, Text.controllerSubtitle);            

            controllerMain.AddItem(mainScoreCollection = new UIMenuListItem(Text.mainScoreCollectionTitle, Collections.scoreCollections, 0, Text.mainScoreCollectionDescr));
            controllerMain.AddItem(mainScoreTrack = new UIMenuListItem(Text.mainScoreTrackTitle, Tracks.listAssault, 0, Text.mainScoreTrackDescr));
            controllerMain.AddItem(mainScoreIntensity = new UIMenuListItem(Text.mainScoreIntensityTitle, Intensities.listIntensities, 0, Text.mainScoreIntensityDescr));
            controllerMain.AddItem(mainMuteSound = new UIMenuCheckboxItem(Text.mainMuteSoundTitle, false, Text.mainMuteSoundDescr));
            controllerMain.AddItem(mainMuteRadio = new UIMenuCheckboxItem(Text.mainMuteRadioTitle, false, Text.mainMuteRadioDescr));

            controllerMain.AddItem(mainCustomEvent = new UIMenuItem(Text.mainCustomEventTitle, Text.mainCustomEventDescr));
            controllerMain.AddItem(mainCustomScene = new UIMenuItem(Text.mainCustomSceneTitle, Text.mainCustomSceneDescr));

            controllerMain.SetBannerType(bannerScoreController); // Adding the banner

            controllerMain.AddInstructionalButton(buttonStopScore); // Adding the Stop Score button

            controllerMenuPool.Add(controllerMain);

            Tick += OnTick;
            KeyDown += OnKeyDown;
            controllerMain.OnIndexChange += OnIndexChange;
            controllerMain.OnItemSelect += OnItemSelect;
            controllerMain.OnListChange += ListChangeHandler;
            controllerMain.OnCheckboxChange += OnCheckboxChange;
            controllerMain.RefreshIndex();
        }

        #region Methods
        static void TriggerEvent(string name) // Triggering a music event
        {
            Function.Call(Hash.TRIGGER_MUSIC_EVENT, name);
        }

        static void StartScene(string name) // Starting an audio scene
        {
            Function.Call(Hash.START_AUDIO_SCENE, name);
            currentAudioScene = name;
        }

        static void StopScene(string name) // Stopping an audio scene
        {
            Function.Call(Hash.STOP_AUDIO_SCENE, name);
            currentAudioScene = null;
        }

        static void PlayScore() // Playing a Score Track
        {
            IsScorePlaying = true;

            string name = currentScoreTrack.Title;
            UI.Notify("Current track is: " + name); // #DEBUG

            TriggerEvent(currentScoreTrack.Event); // Triggering the Track's music event

            string supportingevent = "BTL_IDLE"; // Default supporting Music Event

            switch (currentScoreTrack.Stems) // Determining the supporting Music Event depending on the amount of stems in the current Track
            {
                case 3:
                    supportingevent = Intensities.EventsList3[0];
                    break;
                case 4:
                    supportingevent = Intensities.EventsList4[0];
                    break;
                case 5:
                    supportingevent = Intensities.EventsList5[0];
                    break;
                case 6:
                    supportingevent = Intensities.EventsList6[0];
                    break;
                case 7:
                    supportingevent = Intensities.EventsList7[0];
                    break;
                case 8:
                    supportingevent = Intensities.EventsList8[0];
                    break;
            }

            TriggerEvent(supportingevent); // Supporting Music Event so the track is playing

            mainScoreIntensity.Index = 0; // Changing selected Score Intensity to default
        }

        static void StopScore() // Stopping the playing Score Track
        {
            IsScorePlaying = false;

            TriggerEvent("GTA_ONLINE_STOP_SCORE");

            mainScoreIntensity.Index = 0; // Changing selected Score Intensity to default
        }

        static void SetInstensity() // Controlling the current track's intensity
        {
            if (currentScoreTrack.Stems == 1 || currentScoreTrack.Stems == 2)
                return;

            ScoreIntensity intensity = Intensities.FindIntensity(mainScoreIntensity.Items[mainScoreIntensity.Index].ToString());
            
            if (currentScoreTrack.Stems == 5) // Arena War
            {
                int index = Intensities.IntensitiesList.IndexOf(intensity);

                TriggerEvent(Intensities.EventsList5[index]);
            }
            
            if (currentScoreTrack.Stems == 6) // Assault, Smuggler
            {
                int index = Intensities.IntensitiesList.IndexOf(intensity);

                TriggerEvent(Intensities.EventsList6[index]);
            }

            if (currentScoreTrack.Stems == 7) // Woody Jackson's Sapstick
            {
                int index = Intensities.IntensitiesList.IndexOf(intensity);

                TriggerEvent(Intensities.EventsList7[index]);
            }

            if (currentScoreTrack.Stems == 8) // Doomsday
            {
                int index = Intensities.IntensitiesList.IndexOf(intensity);

                TriggerEvent(Intensities.EventsList8[index]);
            }
        }

        static void MuteSound()
        {
            IsSoundMuted = true;

            StartScene("END_CREDITS_SCENE");
        }

        static void UnmuteSound()
        {
            IsSoundMuted = false;

            StopScene("END_CREDITS_SCENE");
        }

        static void MuteRadio()
        {
            IsRadioMuted = true;

            //StartScene("CAR_MOD_RADIO_MUTE_SCENE");
            StartScene("MIC1_RADIO_DISABLE");
        }

        static void UnmuteRadio()
        {
            IsRadioMuted = false;

            StopScene("MIC1_RADIO_DISABLE");
        }

        static void GetCurrentTrack() // Determining the currently selected Score Track
        {
            currentScoreTrack = Tracks.FindTrack(mainScoreTrack.Items[mainScoreTrack.Index].ToString());
        }

        static bool IsHelpMessageBeingDisplayed() // Checking if a help message is being displayed
        {
            bool isDisplayed = Function.Call<bool>(Hash.IS_HELP_MESSAGE_BEING_DISPLAYED);
            return isDisplayed;
        }

        static bool IsPhoneActive() // Checking if the mobile phone is active
        {
            int id = Function.Call<int>(Hash.PLAYER_PED_ID);
            bool isActive = Function.Call<bool>(Hash.IS_PED_RUNNING_MOBILE_PHONE_TASK, id);
            return isActive;
        }

        static bool IsMenuAvailable() // Checking if everything is good for the menu to be open
        {
            bool isAvailable;

            bool isControlPressed = Game.IsControlPressed(246, GTA.Control.MpTextChatTeam);
            bool isPlayerControllable = Game.Player.CanControlCharacter;
            bool isHelpMessageNotDisplayed = !IsHelpMessageBeingDisplayed();
            bool isPhoneNotActive = !IsPhoneActive();

            if (isControlPressed && isPlayerControllable && isHelpMessageNotDisplayed && isPhoneNotActive)
            {
                isAvailable = true;
            }
            else
            {
                isAvailable = false;
            }

            return isAvailable;
        }

        static bool IsStopScoreAvailable() // Checking if everything is good for the Score to be stopped
        {
            bool isAvailable;

            bool isControlPressed = Game.IsControlPressed(22, GTA.Control.Jump);
            bool isControllerVisible = controllerMain.Visible;

            if (isControlPressed && isControllerVisible && IsScorePlaying)
            {
                isAvailable = true;
            }
            else
            {
                isAvailable = false;
            }

            return isAvailable;
        }

        static bool IsStopSceneAvailable() // Checking if everything is good for the Scene to be stopped
        {
            bool isAvailable;

            bool isControlPressed = Game.IsControlPressed(37, GTA.Control.SelectWeapon);
            bool isControllerVisible = controllerMain.Visible;
            bool isCustomSceneSelected = mainCustomScene.Selected;

            if (isControlPressed && isControllerVisible && isCustomSceneSelected && currentAudioScene != null)
            {
                isAvailable = true;
            }
            else
            {
                isAvailable = false;
            }

            return isAvailable;
        }
    #endregion

        void OnIndexChange(UIMenu sender, int newindex)
        {
            if (sender != controllerMain) return;
           
            GetCurrentTrack(); // Getting current Score Track every time we move throughout the menu

            if (newindex == 6)
            {
                controllerMain.AddInstructionalButton(buttonStopScene); // Adding the Stop Scene button
            }
            else
            {
                controllerMain.RemoveInstructionalButton(buttonStopScene); // Removing the Stop Scene button
            }
        }

        void OnTick(object sender, EventArgs e)
        {
            controllerMenuPool.ProcessMenus();

            if (controllerMain.Visible)
            {
                Game.DisableControlThisFrame(22, GTA.Control.Jump); // Disallowing jumping while in the menu

                if (IsHelpMessageBeingDisplayed())
                {
                    controllerMain.Visible = false;
                }
            }

            if (!controllerMain.Visible)
            {
                controllerMain.RemoveInstructionalButton(buttonStopScene); // Removing the Stop Scene button WORKAROUND BUGFIX
            }

            if (IsScorePlaying) // Disabling set/track selection while a Track is playing
            {
                mainScoreCollection.Enabled = false;
                controllerMain.MenuItems[1].Enabled = false;
                mainScoreIntensity.Enabled = true;
            }
            else
            {
                mainScoreCollection.Enabled = true;
                controllerMain.MenuItems[1].Enabled = true;
                mainScoreIntensity.Enabled = false;
            }

            if (IsRadioMuted)
            {
                Game.DisableControlThisFrame(85, GTA.Control.VehicleRadioWheel); // Disabling radio wheel in vehicles
            }

            // if (currentScoreTrack != null)
            // {
            //     UI.Notify("The current track is: " + currentScoreTrack.Title); // #DEBUG
            // }
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (IsMenuAvailable())
            {
                controllerMain.RefreshIndex();
                controllerMain.Visible = !controllerMain.Visible; // Showing/hiding the menu if Y (by default) is pressed

                switch (controllerMain.Visible)
                {
                    case true:
                        Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1); // Playing sound on menu open
                        break;

                    case false:
                        Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1); // Playing Select sound on menu close as in the original Interaction Menu
                        Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "BACK", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1); // Playing sound on menu close
                        break;
                }
            }

            if (IsStopScoreAvailable())
            {
                // UI.Notify("Score stopped."); // #DEBUG
                StopScore(); // Stopping the currently playing Score Track
            }

            if (IsStopSceneAvailable())
            {
                // UI.Notify("Scene stopped: " + currentAudioScene); // #DEBUG
                StopScene(currentAudioScene); // Stopping the currently playing Audio Scene
            }
        }

        void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            bool hasWentDown = false; // WORKAROUND BUGFIX

            if (selectedItem == mainScoreCollection) // Fancy thing to disallow selecting the Set
            {
                hasWentDown = true;
                mainScoreCollection.Selected = false;
                controllerMain.GoDown();
            }

            if (controllerMain.CurrentSelection == 1 && !hasWentDown)
            {
                PlayScore();
            }

            if (selectedItem == mainScoreIntensity)
            {
                SetInstensity();
            }

            if (selectedItem == mainCustomEvent)
            {
                TriggerEvent(OnscreenKeyboard.GetInput());
            }

            if (selectedItem == mainCustomScene)
            {
                StartScene(OnscreenKeyboard.GetInput());
            }
        }

        void ListChangeHandler(UIMenu sender, UIMenuListItem list, int index)
        {
            if (sender != controllerMain) return;

            if (controllerMain.CurrentSelection == 0)
            {
                switch (index)
                {
                    case 0:
                        mainScoreTrack.Items = Tracks.listAssault;
                        break;
                    case 1:
                        mainScoreTrack.Items = Tracks.listDoomsday;
                        break;
                    case 2:
                        mainScoreTrack.Items = Tracks.listSmuggler;
                        break;
                    case 3:
                        mainScoreTrack.Items = Tracks.listArenaWar;
                        break;
                    case 4:
                        mainScoreTrack.Items = Tracks.listWoodyJackson;
                        break;
                    case 5:
                        mainScoreTrack.Items = Tracks.listArsenyTomilov;
                        break;
                }
            }

            if (controllerMain.CurrentSelection == 1)
            {
                GetCurrentTrack();
            }
        }

        void OnCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked)
        {
            if (sender != controllerMain) return;

            if (checkbox == mainMuteSound)
            {
                switch (Checked)
                {
                    case true:
                        MuteSound();
                        break;
                    case false:
                        UnmuteSound();
                        break;
                }
            }

            if (checkbox == mainMuteRadio)
            {
                switch (Checked)
                {
                    case true:
                        MuteRadio();
                        break;
                    case false:
                        UnmuteRadio();
                        break;
                }
            }
        }
    }
}
