using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;

namespace Score_Controller
{
    public class OnscreenKeyboard
    {
        static string defaulttext = null;

        public static string GetInput()
        {
            string input = Game.GetUserInput(defaulttext, 30);
            defaulttext = input;
            return input;
        }
    }
}
