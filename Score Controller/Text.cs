using GTA;

namespace Score_Controller
{

    public class Text
    {
#if DLCPACK
        public static string controllerTitle = GetGXTEntry("SCUI_TITLE");
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
#else
        public static string controllerTitle = "Score Controller";
        public static string controllerSubtitle = "SCORE CONTROLLER";

        public static string mainScoreCollectionTitle = "Score Set";
        public static string mainScoreCollectionDescr = "Select a Score Set you would like to listen to a track from.";

        public static string mainScoreTrackTitle = "Track";
        public static string mainScoreTrackDescr = "Select a Score Track from the current collection to listen to.";

        public static string mainPlayAssaultDescr = "Select a Score Track from the Southern San Andreas Super Sport Series collection to listen to.";
        public static string mainPlayDoomsdayDescr = "Select a Score Track from the Doomsday Heist collection to listen to.";
        public static string mainPlaySmugglerDescr = "Select a Score Track from the Smuggler's Run collection to listen to.";
        public static string mainPlayArenaWarDescr = "Select a Score Track from the Arena War collection to listen to.";
        public static string mainPlayWoodyJacksonDescr = "Select a Score Track from the Woody Jackson's Tracks collection to listen to.";
        public static string mainPlayArsenyTomilovDescr = "Select a Score Track from the Arseny Tomilov's Tracks collection to listen to.";

        public static string mainScoreIntensityTitle = "Intensity";
        public static string mainScoreIntensityDescr = "Select from the available intensity modes to control the playing track's phase.";

        public static string mainMuteSoundTitle = "Mute Sound";
        public static string mainMuteSoundDescr = "Mute all sound but music.";

        public static string mainMuteRadioTitle = "Mute Radio";
        public static string mainMuteRadioDescr = "Mute radio. Radio Wheel will be disabled in vehicles.";

        public static string mainDisableWantedTitle = "Disable Wanted Music";
        public static string mainDisableWantedDescr = "Disable wanted music that starts playing upon gaining 3 Wanted Stars.";

        public static string mainDisableFlightTitle = "Disable Flight Music";
        public static string mainDisableFlightDescr = "Disable flight music that starts playing upon getting airborne in an aircraft.";

        public static string mainDisableOnDeathTitle = "Stop Score on Death";
        public static string mainDisableOnDeathDescr = "Stop the playing Score Track on player's death.";

        public static string mainCustomEventTitle = "Trigger Music Event";
        public static string mainCustomEventDescr = "Trigger custom Music Event.";

        public static string mainCustomSceneTitle = "Start Audio Scene";
        public static string mainCustomSceneDescr = "Start custom Audio Scene.";

        public static string helpMinigameInProgress = "Score Controller is unavailable during this minigame.";

        public static string helpActivateScoreController = "Press ~INPUT_MP_TEXT_CHAT_TEAM~ to activate Score Controller.";

        public static string buttonStopScore = "Stop Score";
        public static string buttonStopScene = "Stop Audio Scene";
        public static string buttonCancelEvent = "Cancel Music Event";
#endif
    }
}
