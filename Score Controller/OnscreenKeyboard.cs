using GTA;

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
