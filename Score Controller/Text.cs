using GTA;

namespace Score_Controller
{

    public class Text
    {
        public static string controllerTitle = "";
        //public static string controllerTitle = GetGXTEntry("SCUI_TITLE"); // Without custom banner
        public static string controllerSubtitle = GetGXTEntry("SCUI_TITLE_SUB");

        public static string mainScoreCollectionTitle = GetGXTEntry("SCUI_MENUENTRY_SCORESET");
        public static string mainScoreCollectionDescr = GetGXTEntry("SCUI_MENUENTRY_SCORESET_DESCR");

        public static string mainScoreTrackTitle = GetGXTEntry("SCUI_MENUENTRY_TRACK");
        public static string mainScoreTrackDescr = GetGXTEntry("SCUI_MENUENTRY_TRACK_DESCR");

        public static string mainPlayAssaultDescr = "Select a Score Track from the Southern San Andreas Super Sport Series collection to listen to.";
        public static string mainPlayDoomsdayDescr = "Select a Score Track from the Doomsday Heist collection to listen to.";
        public static string mainPlaySmugglerDescr = "Select a Score Track from the Smuggler's Run collection to listen to.";
        public static string mainPlayArenaWarDescr = "Select a Score Track from the Arena War collection to listen to.";
        public static string mainPlayWoodyJacksonDescr = "Select a Score Track from the Woody Jackson's Tracks collection to listen to.";
        public static string mainPlayArsenyTomilovDescr = "Select a Score Track from the Arseny Tomilov's Tracks collection to listen to.";

        public static string mainScoreIntensityTitle = GetGXTEntry("SCUI_MENUENTRY_INTENSITY");
        public static string mainScoreIntensityDescr = GetGXTEntry("SCUI_MENUENTRY_INTENSITY_DESCR");

        public static string mainMuteSoundTitle = GetGXTEntry("SCUI_MENUENTRY_MUTESOUND");
        public static string mainMuteSoundDescr = GetGXTEntry("SCUI_MENUENTRY_MUTESOUND_DESCR");

        public static string mainMuteRadioTitle = GetGXTEntry("SCUI_MENUENTRY_MUTERADIO");
        public static string mainMuteRadioDescr = GetGXTEntry("SCUI_MENUENTRY_MUTERADIO_DESCR");

        public static string mainDisableWantedTitle = GetGXTEntry("SCUI_MENUENTRY_DISABLEWANTEDMUSIC");
        public static string mainDisableWantedDescr = GetGXTEntry("SCUI_MENUENTRY_DISABLEWANTEDMUSIC_DESCR");

        public static string mainDisableFlightTitle = GetGXTEntry("SCUI_MENUENTRY_DISABLEFLIGHTMUSIC");
        public static string mainDisableFlightDescr = GetGXTEntry("SCUI_MENUENTRY_DISABLEFLIGHTMUSIC_DESCR");

        public static string mainDisableOnDeathTitle = GetGXTEntry("SCUI_MENUENTRY_STOPSCOREONDEATH");
        public static string mainDisableOnDeathDescr = GetGXTEntry("SCUI_MENUENTRY_STOPSCOREONDEATH_DESCR");

        public static string mainCustomEventTitle = GetGXTEntry("SCUI_MENUENTRY_TRIGGERMUSICEVENT");
        public static string mainCustomEventDescr = GetGXTEntry("SCUI_MENUENTRY_TRIGGERMUSICEVENT_DESCR");

        public static string mainCustomSceneTitle = GetGXTEntry("SCUI_MENUENTRY_STARTAUDIOSCENE");
        public static string mainCustomSceneDescr = GetGXTEntry("SCUI_MENUENTRY_STARTAUDIOSCENE_DESCR");

        public static string helpMinigameInProgress = GetGXTEntry("SCUI_HELP_MINIGAME");

        public static string helpActivateScoreController = GetGXTEntry("SCUI_HELP_HOTKEY");

        public static string buttonStopScore = GetGXTEntry("SCUI_BUTTON_STOPSCORE");
        public static string buttonStopScene = GetGXTEntry("SCUI_BUTTON_STOPSCENE");
        public static string buttonCancelEvent = GetGXTEntry("SCUI_BUTTON_CANCELEVENT");
             
        private static string GetGXTEntry(string entryname)
        {
            return Game.GetGXTEntry(entryname);
        }
    }
}
