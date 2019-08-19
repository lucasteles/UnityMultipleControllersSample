using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] Joystick JoystickNumber;
    UnityInput input = new UnityInput();


    public float Horizontal => input.GetAxis(JoystickNumber.GetHorizontalAxisName());
    public float Vertical => input.GetAxis(JoystickNumber.GetVerticalAxisName());

    public void AssignJoystick(Joystick joystick) => JoystickNumber = joystick;

    public bool ButtonIsDown(JoystickButton button) =>
          input.GetButton(button.GetInputName(JoystickNumber));
}
