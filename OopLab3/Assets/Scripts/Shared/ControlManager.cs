using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControlManager : MonoBehaviour
{
    public Animator animator;

    public bool playerIsIdle = true;
    public int steps;
    public int player_direction_x;
    public int player_direction_y;

    public  Func<GameObject, bool> moveUp { get; set; }
    public  Func<GameObject, bool> moveDown { get; set; }
    public  Func<GameObject, bool> moveLeft { get; set; }
    public  Func<GameObject, bool> moveRight { get; set; }

    private void Start()
    {
        steps = 0;
        player_direction_x = 0;
        player_direction_y = -1;
    }

    public void Movement(GameObject player)
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (moveUp(player))
            {
                playerIsIdle = false;
                steps++;
            }
            else
                playerIsIdle = true;

            player_direction_x = 0;
            player_direction_y = 1;



        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (moveDown(player))
            {
                playerIsIdle = false;
                steps++;
            }
            else
                playerIsIdle = true;

            player_direction_x = 0;
            player_direction_y = -1;



        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (moveLeft(player))
            {
                playerIsIdle = false;
                steps++;
            }
            else
                playerIsIdle = true;

            player_direction_x = -1;
            player_direction_y = 0;



        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (moveRight(player))
            {
                playerIsIdle = false;
                steps++;
            }
            else
                playerIsIdle = true;
            player_direction_x = 1;
            player_direction_y = 0;
        }

        if (!Input.anyKey)
            playerIsIdle = true;
    }
}
