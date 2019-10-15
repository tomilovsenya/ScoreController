using GTA;
using GTA.Native;

namespace Score_Controller
{
    public class WarningMessage
    {
        public bool IsWithHeader;
        //public string Header;
        public string Line1;
        public int InstructionalKey;
        public string Line2;
        public bool Background;
        public int State;

        public WarningMessage(string line1, int instructionalKey, string line2, bool background, int state)
        {
            // Header = header;
            Line1 = line1;
            InstructionalKey = instructionalKey;
            Line2 = line2;
            Background = background;
            State = state;
        }

        public delegate void WarningMessageEvent(WarningMessage message, int state);

        public static event WarningMessageEvent OnWarningMessage;

        public static void PrepareWarningMessage(WarningMessage message)
        {
            Controller.currentWarningMessage = message;
            Controller.IsWarningMessageActive = true;
            message.State = 0;
        }

        public static void DisplayWarningMessage(WarningMessage message)
        {
            Function.Call(Hash.SET_WARNING_MESSAGE, message.Line1, message.InstructionalKey, message.Line2, false, -1, 0, 0, message.Background);

            Game.Pause(true);

            if (Game.IsControlJustReleased(201, GTA.Control.FrontendAccept))
            {
                message.State = 1;
            }

            if (Game.IsControlJustReleased(202, GTA.Control.FrontendCancel))
            {
                message.State = 2;
            }

            switch (message.State)
            {
                case 0:
                    break;
                case 1:
                    Game.Pause(false);
                    Controller.IsWarningMessageActive = false;
                    break;
                case 2:
                    Game.Pause(false);
                    Controller.IsWarningMessageActive = false;
                    break;
            }

            WarningMessage.OnWarningMessage?.Invoke(message, message.State);
        }

        enum ButtonTypes
        {
            NONE = 0,
            SELECT = 1,
            OK = 2,
            YES = 4,
            BACK = 8,
            BACK_SELECT = 9,
            BACK_OK = 10,
            BACK_YES = 12,
            CANCEL = 16,
            CANCEL_SELECT = 17,
            CANCEL_OK = 18,
            CANCEL_YES = 20,
            NO = 32,
            NO_SELECT = 33,
            NO_OK = 34,
            YES_NO = 36,
            RETRY = 64,
            RETRY_SELECT = 65,
            RETRY_OK = 66,
            RETRY_YES = 68,
            RETRY_BACK = 72,
            RETRY_BACK_SELECT = 73,
            RETRY_BACK_OK = 74,
            RETRY_BACK_YES = 76,
            RETRY_CANCEL = 80,
            RETRY_CANCEL_SELECT = 81,
            RETRY_CANCEL_OK = 82,
            RETRY_CANCEL_YES = 84,
            SKIP = 256,
            SKIP_SELECT = 257,
            SKIP_OK = 258,
            SKIP_YES = 260,
            SKIP_BACK = 264,
            SKIP_BACK_SELECT = 265,
            SKIP_BACK_OK = 266,
            SKIP_BACK_YES = 268,
            SKIP_CANCEL = 272,
            SKIP_CANCEL_SELECT = 273,
            SKIP_CANCEL_OK = 274,
            SKIP_CANCEL_YES = 276,
            CONTINUE = 16384,
            BACK_CONTINUE = 16392,
            CANCEL_CONTINUE = 16400,
            LOADING_SPINNER = 134217728,
            SELECT_LOADING_SPINNER = 134217729,
            OK_LOADING_SPINNER = 134217730,
            YES_LOADING_SPINNER = 134217732,
            BACK_LOADING_SPINNER = 134217736,
            BACK_SELECT_LOADING_SPINNER = 134217737,
            BACK_OK_LOADING_SPINNER = 134217738,
            BACK_YES_LOADING_SPINNER = 134217740,
            CANCEL_LOADING_SPINNER = 134217744,
            CANCEL_SELECT_LOADING_SPINNER = 134217745,
            CANCEL_OK_LOADING_SPINNER = 134217746,
            CANCEL_YES_LOADING_SPINNER = 134217748
        }
        enum MessageStates
        {
            DEFAULT = 0,
            ACCEPTED = 1,
            CANCELED = 2
        }
    }
}
