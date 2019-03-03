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

namespace Score_Controller
{
    public class Controller : Script
    {
        MenuPool controllerMenuPool;
        UIMenu controllerMain;

        private static UIMenuListItem mainScoreCollection;

        private static UIMenuListItem mainSetAssault;
        private static UIMenuListItem mainSetDoomsday;
        private static UIMenuListItem mainSetSmuggler;
        private static UIMenuListItem mainSetArenaWar;

        private static UIMenuListItem mainScoreIntensity;
        private static UIMenuCheckboxItem mainMuteSound;
        private static UIMenuCheckboxItem mainMuteRadio;

        private static bool IsScorePlaying = false; // The field to tell if a Track is playing
        private static bool IsSoundMuted = false; // The field to tell if sound is muted; ВОЗМОЖНО, НЕ ПРИГОДИТСЯ
        private static bool IsRadioMuted = false; // The field to tell if radio is muted

        private static ScoreTrack currentScoreTrack = Tracks.TracksList[0]; // Currently selected Score Track

        public Controller()
        {
            Tracks.AddTracks(); // Adding all tracks
            Collections.AddCollections(); // Adding all collections

            controllerMenuPool = new MenuPool();
            controllerMain = new UIMenu(Text.controllerTitle, Text.controllerSubtitle);            

            controllerMain.AddItem(mainScoreCollection = new UIMenuListItem(Text.mainScoreCollectionTitle, Collections.scoreCollections, 0, Text.mainScoreCollectionDescr));

            controllerMain.AddItem(mainSetAssault = new UIMenuListItem(Text.mainPlayScoreTitle, Tracks.tracksAssault, 0, Text.mainPlayAssaultDescr));
            mainSetDoomsday = new UIMenuListItem(Text.mainPlayScoreTitle, Tracks.tracksDoomsday, 0, Text.mainPlayDoomsdayDescr);
            mainSetSmuggler = new UIMenuListItem(Text.mainPlayScoreTitle, Tracks.tracksSmuggler, 0, Text.mainPlaySmugglerDescr);
            mainSetArenaWar = new UIMenuListItem(Text.mainPlayScoreTitle, Tracks.tracksArenaWar, 0, Text.mainPlayArenaWarDescr);

            controllerMain.AddItem(mainScoreIntensity = new UIMenuListItem(Text.mainScoreIntensityTitle, Tracks.scoreInts, 0, Text.mainScoreIntensityDescr));
            controllerMain.AddItem(mainMuteSound = new UIMenuCheckboxItem(Text.mainMuteSoundTitle, false, Text.mainMuteSoundDescr));
            controllerMain.AddItem(mainMuteRadio = new UIMenuCheckboxItem(Text.mainMuteRadioTitle, false, Text.mainMuteRadioDescr));
            
            var buttonStopScore = new InstructionalButton(GTA.Control.Jump, "Stop Score"); // Creating the Stop Score button
            controllerMain.AddInstructionalButton(buttonStopScore); // Adding the Stop Score button

            controllerMenuPool.Add(controllerMain);

            Tick += onTick;
            KeyDown += onKeyDown;
            controllerMain.OnItemSelect += OnItemSelect;
            controllerMain.OnListChange += ListChangeHandler;
            controllerMain.OnCheckboxChange += OnCheckboxChange;
            controllerMain.RefreshIndex();
        }

        #region 
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

        static void PlayScore(string collection, int number, bool isStandard) // Playing a Score Track
        { 
            IsScorePlaying = true;

            string name = null;

            switch (isStandard) // #DEBUG
            {
                case false:
                    UI.Notify("Not standard naming.");
                    break;
                case true:
                    UI.Notify("Standard naming.");
                    break;
            }

            if (isStandard)
            {
                name = "RC_" + collection + "_" + number.ToString();
            }
            else
            {
                name = collection + "_" + number.ToString();
            }

            UI.Notify("Current track is: " + name); // #DEBUG

            TriggerEvent(name);
            // GTA.Native.Function.Call(Hash.PREPARE_MUSIC_EVENT, "GTA_ONLINE_STOP_SCORE"); // Узнать, помогает ли это
            TriggerEvent("BTL_IDLE"); // Supporting Music Event so the track is playing

            mainScoreIntensity.Index = 0; // Changing selected Score Intensity to default
        }

        static void PlayScoreByEventName(string name) // Playing a Score Track by its event name
        {
            IsScorePlaying = true;

            TriggerEvent(name);
            // GTA.Native.Function.Call(Hash.PREPARE_MUSIC_EVENT, "GTA_ONLINE_STOP_SCORE"); // Узнать, помогает ли это
            TriggerEvent("BTL_IDLE"); // Supporting Music Event so the track is playing

            mainScoreIntensity.Index = 0; // Changing selected Score Intensity to default
        }

        static void StopScore() // Stopping the playing Score Track
        {
            IsScorePlaying = false;

            TriggerEvent("GTA_ONLINE_STOP_SCORE");

            mainScoreIntensity.Index = 0; // Changing selected Score Intensity to default
        }

        static void SetInstensity(int intensity, int stems) // Controlling the current track's intensity; ЗАПИХНУТЬ СЮДА ЗАВИСИМОСТЬ ОТ КОЛ-ВА СТЕМОВ
        {
            string low = null;
            string mid = null;
            string high = null;


            if (stems == 1 || stems == 2)
                return;

            if (stems == 3)
            {

            }

            if (stems == 4)
            {

            }

            if (stems == 5)
            {

            }

            if (stems == 6)
            {
                low = "BTL_IDLE";
                mid = "BTL_MED_INTENSITY";
                high = "BTL_GUNFIGHT";
            }

            if (stems == 7) // WDY_SAPSTICK
            {

            }

            if (stems == 8)
            {
                low = "BKR_GUNRUN_DEAL";
                mid = "BG_SIGHTSEER_MID";
                high = "DROPZONE_ACTION_HIGH";
            }

            switch (intensity)
            {
                case 0:
                    TriggerEvent(low); // Low
                    break;
                case 1:
                    TriggerEvent(mid); // Mid
                    break;
                case 2:
                    TriggerEvent(high); // High
                    break;
                case 3:
                    TriggerEvent("AW_ANNOUNCER_FINISHED"); // Stems 1-5
                    break;
                case 4:
                    TriggerEvent("BKR_SAFECRACKER_CRACK"); // Stem 3
                    break;
                case 5:
                    TriggerEvent("CMH_MP_STEALTH"); // Stealth (linear mixing)
                    break;
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
    #endregion

        void onTick(object sender, EventArgs e)
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
        }

        void onKeyDown(object sender, KeyEventArgs e)
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

            if (sender == controllerMain)
            {
                string collection = Collections.CollectionsList[mainScoreCollection.Index].Event;
                
                if (selectedItem == mainSetAssault) // Determining which Score Track is selected and playing it
                {
                    int currentIndex = mainSetAssault.Index + 1;
                    PlayScore(collection, currentIndex, true);
                }

                if (selectedItem == mainSetDoomsday) // Determining which Score Track is selected and playing it
                {
                    int currentIndex = mainSetDoomsday.Index + 1;
                    PlayScore(collection, currentIndex, true);
                }

                if (selectedItem == mainSetSmuggler) // Determining which Score Track is selected and playing it
                {
                    int currentIndex = mainSetSmuggler.Index + 1;
                    PlayScore(collection, currentIndex, true);
                }

                if (selectedItem == mainSetArenaWar) // Determining which Score Track is selected and playing it
                {
                    int currentIndex = mainSetArenaWar.Index;

                    if (currentIndex == 0) // Playing the exception (Main Theme)
                    {
                        PlayScoreByEventName("AW_LOBBY_MUSIC_START_STA");
                    }
                    else // Playing normal tracks
                    {
                        PlayScore(collection, currentIndex, false);
                    }
                }

                if (selectedItem == mainScoreIntensity)
                {
                    int stems = Tracks.FindTrack(selectedItem.Text).Stems;
                    SetInstensity(mainScoreIntensity.Index, stems);
                }

                currentScoreTrack = Tracks.FindTrack(selectedItem.Text); // Determining the selected track

                UI.Notify("Current collection is: " + collection); // #DEBUG
            }
        }

        void ListChangeHandler(UIMenu sender, UIMenuListItem list, int index)
        {
            if (sender != controllerMain || list != mainScoreCollection) return;

            switch (index) // Setting the needed tracklist based on the selected Score Set
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
