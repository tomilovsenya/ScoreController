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
        public static ScoreTrack Assault_One = new ScoreTrack(Collections.Assault, "dlc_assault_tech_1", "Assault Tech One", "RC_ASSAULT_1", 6);
        public static ScoreTrack Assault_Two = new ScoreTrack(Collections.Assault, "dlc_assault_tech_2", "Assault Tech Two", "RC_ASSAULT_2", 6);
        public static ScoreTrack Assault_Three = new ScoreTrack(Collections.Assault, "dlc_assault_tech_3", "Assault Tech Three", "RC_ASSAULT_3", 6);
        public static ScoreTrack Assault_Four = new ScoreTrack(Collections.Assault, "dlc_assault_tech_4", "Assault Tech Four", "RC_ASSAULT_4", 6);

        public static ScoreTrack Doomsday_One = new ScoreTrack(Collections.Doomsday, "cmh_heist_1", "CMH Heist One", "RC_HEIST2_1", 8);
        public static ScoreTrack Doomsday_Two = new ScoreTrack(Collections.Doomsday, "cmh_heist_2", "CMH Heist Two", "RC_HEIST2_2", 8);

        public static ScoreTrack Smuggler_INF = new ScoreTrack(Collections.Smuggler, "smuggler_track_inf", "Smuggler Track INF", "RC_SMUGGLER_1", 6);
        public static ScoreTrack Smuggler_One = new ScoreTrack(Collections.Smuggler, "smuggler_track_s1", "Smuggler Track S1", "RC_SMUGGLER_2", 6);

        public static ScoreTrack ArenaWar_Theme = new ScoreTrack(Collections.ArenaWar, "dlc_awxm2018_theme_5_stems", "Arena War Theme", "AW_LOBBY_MUSIC_START_STA", 5);
        public static ScoreTrack ArenaWar_One = new ScoreTrack(Collections.ArenaWar, "dlc_aw_track_1", "AW Track One", "MC_AW_MUSIC_1", 5);
        public static ScoreTrack ArenaWar_Two = new ScoreTrack(Collections.ArenaWar, "dlc_aw_track_2", "AW Track Two", "MC_AW_MUSIC_2", 5);

        public static ScoreTrack Sapstick = new ScoreTrack(Collections.WoodyJackson, "wdy_sapstick", "Sapstick", "MIC2_START", 7);

        public static List<ScoreTrack> TrackList = new List<ScoreTrack>()
        {
            Assault_One,
            Assault_Two,
            Assault_Three,
            Assault_Four,

            Doomsday_One,
            Doomsday_Two,

            Smuggler_INF,
            Smuggler_One,

            ArenaWar_Theme,
            ArenaWar_One,
            ArenaWar_Two,

            Sapstick
        };

        public static void AddTracks() // Adding tracks; a #NEWCOLLECTION must be added to this list
        {
            foreach (ScoreTrack track in TrackList)
            {
                if (track.Collection == Collections.Assault)
                {
                    listAssault.Add(track.Title);
                }
                if (track.Collection == Collections.Doomsday)
                {
                    listDoomsday.Add(track.Title);
                }
                if (track.Collection == Collections.Smuggler)
                {
                    listSmuggler.Add(track.Title);
                }
                if (track.Collection == Collections.ArenaWar)
                {
                    listArenaWar.Add(track.Title);
                }
                if (track.Collection == Collections.WoodyJackson)
                {
                    listWoodyJackson.Add(track.Title);
                }
            }
        }

        public static ScoreTrack FindTrack(string name)
        {
            return TrackList.Find(track => track.Title == name);
        }

        public static List<object> listAssault = new List<object>();
        public static List<object> listDoomsday = new List<object>();
        public static List<object> listSmuggler = new List<object>();
        public static List<object> listArenaWar = new List<object>();
        public static List<object> listWoodyJackson = new List<object>();

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
