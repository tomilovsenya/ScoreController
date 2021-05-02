using System;
using GTA;
using GTA.Native;
using NativeUI;
using System.Windows.Forms;
using System.Drawing;

namespace Score_Controller
{
#if DEBUG
    public class Testing : Script
    {
        private static UIMenu testingMain;
        private static UIMenuItem testingFinaleTrack;

        public Testing()
        {
            testingMain = new UIMenu("Testing", "TESTING OPTIONS");

            testingMain.AddItem(testingFinaleTrack = new UIMenuItem("Trigger CH Finale Track", "Testing stuff.")); // #DEBUG
            // Controller.controllerMain.AddItem(testingMain); // #DEBUG
        }
    }
#endif
}