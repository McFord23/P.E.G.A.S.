public static class Save
{
    public static bool TogetherMode = false;

    public struct Player1
    {
        public static string character = "Celestia";
        public static string controlLayout = "mouse";
        public static int gamepad = 1;
        public static bool live = true;
    }

    public struct Player2
    {
        public static string character = "Luna";
        public static string controlLayout = "numpad";
        public static int gamepad = 2;
        public static bool live = true;
    }
    
    public struct Sensitivity
    {
        public static float mouse = 0.4f;
        public static float keyboard = 6f;
        public static float gamepad = 6f;
    }
    
    public static bool sound = true;
    public static bool music = false;
}
