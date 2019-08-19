public enum JoystickButton { A, B, X, Y }
public enum Joystick { _1, _2, _3, _4, _5, _6 }

public static class JoystickExtensions
{
    public static string GetName(this JoystickButton button, int joystick) =>
        $"J{joystick}.{button}";

    public static string GetName(this Joystick joystick, JoystickButton button) =>
        $"J{joystick.ToString().Substring(1)}.{button}";

}

