using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    public int Speed;
    PlayerInput input;

    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {

        var moveX = input.Horizontal;
        var moveY = input.Vertical;

        var speed = Speed * Time.deltaTime;

        transform.Translate(moveX * speed, 0, 0);
        transform.Translate(0, 0, moveY * speed);




    }
}
