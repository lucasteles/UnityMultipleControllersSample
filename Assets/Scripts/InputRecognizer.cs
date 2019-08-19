using System;
using System.Collections.Generic;
using System.Linq;

public class InputRecognizer
{
    readonly UnityInput unityInput;

    public InputRecognizer(UnityInput unityInput)
    {
        this.unityInput = unityInput;
    }


    public enum JoystickButton { A, B, X, Y }
    public enum Joystick { _1, _2, _3, _4, _5, _6 }


    public string GetButtonName(int joystick, JoystickButton button) =>
        $"J{joystick}.{button}";


    public IEnumerable<Joystick> GetJoystickWichPress(params JoystickButton[] buttons)
    {
        for (var joyIndex = 1; joyIndex <= 6; joyIndex++)
        {


            if (!buttons.Any())
                buttons = Enum.GetValues(typeof(JoystickButton)).Cast<JoystickButton>().ToArray();

            var pressed =
                buttons
                    .Select(button => GetButtonName(joyIndex, button))
                    .Any(unityInput.GetButton);

            if (pressed)
                yield return (Joystick)Enum.Parse(typeof(Joystick), $"_{joyIndex}");

        }

    }


}
