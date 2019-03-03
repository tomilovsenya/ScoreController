using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score_Controller
{
    public class ScoreTrack
    {
        public ScoreCollection Collection; // Collection the track belongs to
        public string Name; // Actual technical name of the track (from the gamefiles)
        public string Title; // Displayed name of the track (shorter is better)
        public int Stems; // Number of track's stems (not always the same for all the tracks in a collection)

        public ScoreTrack(ScoreCollection collection, string name, string title, int stems)
        {
            Collection = collection;
            Name = name;
            Title = title;
            Stems = stems;
        }
    }
}
