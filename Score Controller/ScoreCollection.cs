using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score_Controller
{
    public class ScoreCollection
    {
        public string Name; // Actual name of the collection (DLC name in most cases)
        public string Title; // Displayed name of the collection (shorter is better)
        public string Event; // Music event of the collection

        public ScoreCollection(string name, string title, string eventname)
        {
            Name = name;
            Title = title;
            Event = eventname;
        }
    }
}
