using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace Score_Controller
{
    public class Collections
    {
        public static ScoreCollection Assault = new ScoreCollection("Southern San Andreas Super Sport Series", "Assault", "ASSAULT");
        public static ScoreCollection Doomsday = new ScoreCollection("The Doomsday Heist", "Doomsday", "HEIST2");
        public static ScoreCollection Smuggler = new ScoreCollection("Smuggler's Run", "Smuggler's Run", "SMUGGLER");
        public static ScoreCollection ArenaWar = new ScoreCollection("Arena War", "Arena War", "MC_AW_MUSIC");
        public static ScoreCollection WoodyJackson = new ScoreCollection("Woody Jackson's Tracks", "Woody Jackson", null);

        public static List<ScoreCollection> CollectionsList = new List<ScoreCollection>() // A #NEWCOLLECTION must be added to this list
        {
            Assault,
            Doomsday,
            Smuggler,
            ArenaWar,
            WoodyJackson
        };

        public static void AddCollections() // Adding collections
        {
            int count = 0; // #DEBUG
            foreach (ScoreCollection collection in CollectionsList)
            {
                count++; // #DEBUG
                scoreCollections.Add(collection.Title);
                UI.Notify("Added " + count + " collections."); // #DEBUG
            }
        }

        public static List<object> scoreCollections = new List<object>(); // List of all the Collections for Controller.mainScoreCollection
    }
}
