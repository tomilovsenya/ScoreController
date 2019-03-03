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
        public static List<ScoreCollection> CollectionsList = new List<ScoreCollection>()
        {
            new ScoreCollection("Southern San Andreas Super Sport Series", "Assault", "ASSAULT"),
            new ScoreCollection("The Doomsday Heist", "Doomsday", "HEIST2"),
            new ScoreCollection("Smuggler's Run", "Smuggler's Run", "SMUGGLER"),
            new ScoreCollection("Arena War", "Arena War", "MC_AW_MUSIC")
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
