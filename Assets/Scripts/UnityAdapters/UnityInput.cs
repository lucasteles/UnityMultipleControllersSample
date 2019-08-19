using UnityEngine;

public class UnityInput
{
    public virtual bool GetButton(string buttonName) => Input.GetButton(buttonName);

}
