﻿using UnityEngine;

public class Move : MonoBehaviour
{
    public int Speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var moveX = Input.GetAxis("J1.Horizontal");
        var moveY = Input.GetAxis("J1.Vertical");

        var speed = Speed * Time.deltaTime;

        transform.Translate(moveX * speed, 0, 0);
        transform.Translate(0, 0, moveY * speed);


        for (int c = 1; c < 7; c++)
        {

            var t = Input.GetButton($"J{c}.A");

            if (t)
                print($"controller {c} - pressed!");

        }


    }
}
