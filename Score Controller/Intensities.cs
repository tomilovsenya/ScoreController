using GTA;
using System.Collections.Generic;

namespace Score_Controller
{
    public class Intensities
    {
        public static ScoreIntensity Low = new ScoreIntensity("Low", "Low", "BTL_IDLE");
        public static ScoreIntensity Mid = new ScoreIntensity("Mid", "Mid", "BTL_MED_INTENSITY");
        public static ScoreIntensity High = new ScoreIntensity("High", "High", "BTL_GUNFIGHT");

        public static List<ScoreIntensity> IntensitiesList = new List<ScoreIntensity>()
        {
            Low,
            Mid,
            High
        };

        public static List<string> EventsList3 = new List<string>() // Events for tracks consisting of 3 stems; the amount of events here must be equal to the amount of items in IntensitiesList
        {

        };

        public static List<string> EventsList4 = new List<string>() // Events for tracks consisting of 4 stems; the amount of events here must be equal to the amount of items in IntensitiesList
        {
            "FAM2_COMING",
            "FAM2_LOST_HIM",
            "FAM2_SHOOTING"
        };

        public static List<string> EventsList5 = new List<string>() // Events for tracks consisting of 5 stems; the amount of events here must be equal to the amount of items in IntensitiesList
        {
            "AW_LOBBY_VEHICLE_SELECTION",
            "MC_AW_ALL_ON",
            "AW_ANNOUNCER_FINISHED"
        };

        public static List<string> EventsList6 = new List<string>() // Events for tracks consisting of 6 stems; the amount of events here must be equal to the amount of items in IntensitiesList
        {
            "BTL_SUSPENSE",
            "BTL_MED_INTENSITY",
            "BTL_VEHICLE_ACTION"
        };

        public static List<string> EventsList7 = new List<string>() // Events for tracks consisting of 7 stems; the amount of events here must be equal to the amount of items in IntensitiesList
        {
            "MIC2_START",
            "MIC3_FRANK_DOWN",
            "MIC2_LOSE_TRIADS"
        };

        public static List<string> EventsList8 = new List<string>() // Events for tracks consisting of 8 stems; the amount of events here must be equal to the amount of items in IntensitiesList
        {
            "BKR_GUNRUN_DEAL",
            "BG_SIGHTSEER_MID",
            "DROPZONE_ACTION_HIGH"
        };

        public static void AddIntensities() // Adding intensities
        {
            // int count = 0; // #DEBUG
            foreach (ScoreIntensity collection in IntensitiesList)
            {
                // count++; // #DEBUG
                listIntensities.Add(collection.Title);
                // UI.Notify("Added " + count + " intensities."); // #DEBUG
            }
        }

        public static ScoreIntensity FindIntensity(string name)
        {
            return IntensitiesList.Find(track => track.Title == name);
        }

        public static List<object> listIntensities = new List<object>();
    }
}
