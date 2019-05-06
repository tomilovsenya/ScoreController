using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;

namespace Score_Controller
{

    public class Text
    {
        public static string controllerTitle = "Score Controller";
        public static string controllerSubtitle = "SCORE CONTROLLER";

        public static string mainScoreCollectionTitle = "Score Set";
        public static string mainScoreCollectionDescr = "Select a Score Set you would like to listen to a track from.";

        public static string mainPlayScoreTitle = "Track";
        public static string mainPlayAssaultDescr = "Select a Score Track from the Southern San Andreas Super Sport Series collection to listen to.";
        public static string mainPlayDoomsdayDescr = "Select a Score Track from the Doomsday Heist collection to listen to.";
        public static string mainPlaySmugglerDescr = "Select a Score Track from the Smuggler's Run collection to listen to.";
        public static string mainPlayArenaWarDescr = "Select a Score Track from the Arena War collection to listen to.";
        public static string mainPlayWoodyJacksonDescr = "Select a Score Track from the Woody Jackson's Tracks collection to listen to.";

        public static string mainScoreIntensityTitle = "Intensity";
        public static string mainScoreIntensityDescr = "Select from the available intensity modes to control the playing track's phase.";

        public static string mainMuteSoundTitle = "Mute Sound";
        public static string mainMuteSoundDescr = "Mute all sound but music.";

        public static string mainMuteRadioTitle = "Mute Radio";
        public static string mainMuteRadioDescr = "Mute radio.";
       
        void Main()
        {
            if (Game.Language.Equals(0))
            {

            }
            else if (Game.Language.Equals(7))
            {

            }
        }

    }
}
