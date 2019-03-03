using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace Score_Controller
{
    public class Tracks
    {
        public static List<ScoreTrack> TracksList = new List<ScoreTrack>()
        {
            new ScoreTrack(Collections.CollectionsList[0], "dlc_assault_tech_1", "Assault Tech One", 6),
            new ScoreTrack(Collections.CollectionsList[0], "dlc_assault_tech_2", "Assault Tech Two", 6),
            new ScoreTrack(Collections.CollectionsList[0], "dlc_assault_tech_3", "Assault Tech Three", 6),
            new ScoreTrack(Collections.CollectionsList[0], "dlc_assault_tech_4", "Assault Tech Four", 6),

            new ScoreTrack(Collections.CollectionsList[1], "cmh_heist_1", "CMH Heist One", 8),
            new ScoreTrack(Collections.CollectionsList[1], "cmh_heist_2", "CMH Heist Two", 8),

            new ScoreTrack(Collections.CollectionsList[2], "smuggler_track_inf", "Smuggler Track INF", 6),
            new ScoreTrack(Collections.CollectionsList[2], "smuggler_track_s1", "Smuggler Track S1", 6),

            new ScoreTrack(Collections.CollectionsList[3], "dlc_aw_track_1", "AW Track One", 5),
            new ScoreTrack(Collections.CollectionsList[3], "dlc_aw_track_2", "AW Track Two", 5)
        };

        public static void AddTracks() // Adding tracks
        {
            int count = 0; // #DEBUG
            foreach (ScoreTrack track in TracksList)
            {
                count++; // #DEBUG

                var collection = new List<object>();

                switch (track.Collection.Title)
                {
                    case "Assault":
                        collection = tracksAssault;
                        break;
                    case "Doomsday":
                        collection = tracksDoomsday;
                        break;
                    case "Smuggler's Run":
                        collection = tracksSmuggler;
                        break;
                    case "Arena War":
                        collection = tracksArenaWar;
                        break;
                }

                collection.Add(track.Title);

                UI.Notify("Added " + count + " tracks."); // #DEBUG
            }
        }

        public static ScoreTrack FindTrack(string name)
        {
            return TracksList.Find(track => track.Title == name);
        }

        public static List<object> tracksAssault = new List<object>();

        public static List<object> tracksDoomsday = new List<object>();

        public static List<object> tracksSmuggler = new List<object>();

        public static List<object> tracksArenaWar = new List<object>();

        public static List<object> scoreInts = new List<object>
        {
            "Low",
            "Mid",
            "High",
            "Stems 1-5",
            "Stem 3",
            "Stealth"
        };
    }
}
