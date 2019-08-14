using GTA;

namespace Score_Controller
{
    public class OnscreenKeyboard
    {
        static string defaulttext = null;

        public static string GetInput()
        {
            WindowTitle SCUI_CUSTOM_MUSIC_EVENT = new WindowTitle();
            string input = Game.GetUserInput(SCUI_CUSTOM_MUSIC_EVENT, defaulttext, 45);
            defaulttext = input;
            return input;
        }
    }
}
