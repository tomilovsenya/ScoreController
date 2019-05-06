using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score_Controller
{
    public class ScoreIntensity
    {
        public string Name; // Name of the intensity
        public string Title; // Displayed name of the intensity (shorter is better)
        public string Event; // Music event of the intensity

        public ScoreIntensity(string name, string title, string eventname)
        {
            Name = name;
            Title = title;
            Event = eventname;
        }
    }
}
