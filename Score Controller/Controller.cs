using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using GTA.Math;
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

        private static UIMenuListItem mainSetAssault;
        private static UIMenuListItem mainSetDoomsday;
        private static UIMenuListItem mainSetSmuggler;
        private static UIMenuListItem mainSetArenaWar;
        private static UIMenuListItem mainSetWoodyJackson;
        private static UIMenuListItem mainSetArsenyTomilov;

        private static UIMenuListItem mainScoreIntensity;
        private static UIMenuCheckboxItem mainMuteSound;
        private static UIMenuCheckboxItem mainMuteRadio;

        private static UIMenuItem mainCustomEvent;

        private static bool IsScorePlaying = false; // The field to tell if a Track is playing
        private static bool IsSoundMuted = false; // The field to tell if sound is muted; ВОЗМОЖНО, НЕ ПРИГОДИТСЯ
        private static bool IsRadioMuted = false; // The field to tell if radio is muted

        private static ScoreTrack currentScoreTrack = null; // Currently selected Score Track

        public Controller()
        {
            Tracks.AddTracks(); // Adding all tracks
            Collections.AddCollections(); // Adding all collections
            Intensities.AddIntensities(); // Adding all intensities

            controllerMenuPool = new MenuPool();
            controllerMain = new UIMenu(Text.controllerTitle, Text.controllerSubtitle);            

            controllerMain.AddItem(mainScoreCollection = new UIMenuListItem(Text.mainScoreCollectionTitle, Collections.scoreCollections, 0, Text.mainScoreCollectionDescr));

            controllerMain.AddItem(mainSetAssault = new UIMenuListItem(Text.mainPlayScoreTitle, Tracks.listAssault, 0, Text.mainPlayAssaultDescr));
            mainSetDoomsday = new UIMenuListItem(Text.mainPlayScoreTitle, Tracks.listDoomsday, 0, Text.mainPlayDoomsdayDescr);
            mainSetSmuggler = new UIMenuListItem(Text.mainPlayScoreTitle, Tracks.listSmuggler, 0, Text.mainPlaySmugglerDescr);
            mainSetArenaWar = new UIMenuListItem(Text.mainPlayScoreTitle, Tracks.listArenaWar, 0, Text.mainPlayArenaWarDescr);
            mainSetWoodyJackson = new UIMenuListItem(Text.mainPlayScoreTitle, Tracks.listWoodyJackson, 0, Text.mainPlayWoodyJacksonDescr);
            mainSetArsenyTomilov = new UIMenuListItem(Text.mainPlayScoreTitle, Tracks.listArsenyTomilov, 0, Text.mainPlayArsenyTomilovDescr);

            controllerMain.AddItem(mainScoreIntensity = new UIMenuListItem(Text.mainScoreIntensityTitle, Intensities.listIntensities, 0, Text.mainScoreIntensityDescr));
            controllerMain.AddItem(mainMuteSound = new UIMenuCheckboxItem(Text.mainMuteSoundTitle, false, Text.mainMuteSoundDescr));
            controllerMain.AddItem(mainMuteRadio = new UIMenuCheckboxItem(Text.mainMuteRadioTitle, false, Text.mainMuteRadioDescr));

            controllerMain.AddItem(mainCustomEvent = new UIMenuItem(Text.mainCustomEventTitle, Text.mainCustomEventDescr));

            var bannerScoreController = new Sprite("shopui_title_scorecontroller", "shopui_title_scorecontroller", new Point(0, 0), new Size(0, 0)); // Creating the banner
            controllerMain.SetBannerType(bannerScoreController); // Adding the banner

            var buttonStopScore = new InstructionalButton(GTA.Control.Jump, "Stop Score"); // Creating the Stop Score button
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

        public List<UIMenuListItem> CollectionLists = new List<UIMenuListItem>()
        {
            mainSetAssault,
            mainSetDoomsday,
            mainSetSmuggler,
            mainSetArenaWar
        };

        #region Methods
        static void TriggerEvent(string name) // Triggering a music event
        {
            Function.Call(Hash.TRIGGER_MUSIC_EVENT, name);
        }

        static void StartScene(string name) // Starting an audio scene
        {
            Function.Call(Hash.START_AUDIO_SCENE, name);
        }

        static void StopScene(string name) // Stopping an audio scene
        {
            Function.Call(Hash.STOP_AUDIO_SCENE, name);
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

        static UIMenuListItem GetCurrentSet() // Determining the currently selected Score Set; a #NEWCOLLECTION must be added to this list
        {
            UIMenuListItem set = null;

            if (controllerMain.MenuItems[1].Equals(mainSetAssault))
            {
                set = mainSetAssault;
            }
            else if (controllerMain.MenuItems[1].Equals(mainSetDoomsday))
            {
                set = mainSetDoomsday;
            }
            else if (controllerMain.MenuItems[1].Equals(mainSetSmuggler))
            {
                set = mainSetSmuggler;
            }
            else if (controllerMain.MenuItems[1].Equals(mainSetArenaWar))
            {
                set = mainSetArenaWar;
            }
            else if (controllerMain.MenuItems[1].Equals(mainSetWoodyJackson))
            {
                set = mainSetWoodyJackson;
            }
            else if (controllerMain.MenuItems[1].Equals(mainSetArsenyTomilov))
            {
                set = mainSetArsenyTomilov;
            }
            return set;
        }

        static void GetCurrentTrack() // Determining the currently selected Score Track
        {
            currentScoreTrack = Tracks.FindTrack(GetCurrentSet().Items[GetCurrentSet().Index].ToString());
        }
    #endregion

        void OnIndexChange(UIMenu sender, int newindex)
        {
            if (sender != controllerMain) return;
           
            GetCurrentTrack(); // Getting current Score Track every time we move throughout the menu
        }

        void OnTick(object sender, EventArgs e)
        {
            controllerMenuPool.ProcessMenus();

            if (controllerMain.Visible)
            {
                Game.DisableControlThisFrame(22, GTA.Control.Jump); // Disallowing jumping while in the menu
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
            if (Game.IsControlPressed(246, GTA.Control.MpTextChatTeam) && Game.Player.CanControlCharacter)
            {
                controllerMain.RefreshIndex();
                controllerMain.Visible = !controllerMain.Visible; // Showing/hiding the menu if Y (by default) is pressed
            }

            if (controllerMain.Visible && Game.IsControlPressed(22, GTA.Control.Jump) && IsScorePlaying)
            {
                UI.Notify("Score stopped."); // #DEBUG
                StopScore(); // Stopping the currently playing Score Track
            }
        }

        void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (selectedItem == mainScoreCollection) // Fancy thing to disallow selecting the Set
            {
                mainScoreCollection.Selected = false;
                controllerMain.GoDown();
            }

            if (controllerMain.CurrentSelection == 1)
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
        }

        void ListChangeHandler(UIMenu sender, UIMenuListItem list, int index)
        {
            if (sender != controllerMain) return;

            if (controllerMain.CurrentSelection == 0)
            {
                switch (index) // Setting the needed tracklist based on the selected Score Set; if there's a #NEWCOLLECTION, this list needs to be changed
                {
                    case 0:
                        controllerMain.RemoveItemAt(1);
                        controllerMain.AddItemAt(mainSetAssault, 1);
                        controllerMain.RefreshIndex();
                        break;
                    case 1:
                        controllerMain.RemoveItemAt(1);
                        controllerMain.AddItemAt(mainSetDoomsday, 1);
                        controllerMain.RefreshIndex();
                        break;
                    case 2:
                        controllerMain.RemoveItemAt(1);
                        controllerMain.AddItemAt(mainSetSmuggler, 1);
                        controllerMain.RefreshIndex();
                        break;
                    case 3:
                        controllerMain.RemoveItemAt(1);
                        controllerMain.AddItemAt(mainSetArenaWar, 1);
                        controllerMain.RefreshIndex();
                        break;
                    case 4:
                        controllerMain.RemoveItemAt(1);
                        controllerMain.AddItemAt(mainSetWoodyJackson, 1);
                        controllerMain.RefreshIndex();
                        break;
                    case 5:
                        controllerMain.RemoveItemAt(1);
                        controllerMain.AddItemAt(mainSetArsenyTomilov, 1);
                        controllerMain.RefreshIndex();
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
            //if (sender != controllerMain) return;

            if (checkbox == mainMuteSound)
            {
                switch (Checked)
                {
                    case true:
                        UI.Notify("Sound muted"); // #DEBUG
                        MuteSound();
                        break;
                    case false:
                        UI.Notify("Sound unmuted"); // #DEBUG
                        UnmuteSound();
                        break;
                }
            }

            if (checkbox == mainMuteRadio)
            {
                switch (Checked)
                {
                    case true:
                        UI.Notify("Radio muted"); // #DEBUG
                        MuteRadio();
                        break;
                    case false:
                        UI.Notify("Radio unmuted"); // #DEBUG
                        UnmuteRadio();
                        break;
                }
            }
        }
    }
}
