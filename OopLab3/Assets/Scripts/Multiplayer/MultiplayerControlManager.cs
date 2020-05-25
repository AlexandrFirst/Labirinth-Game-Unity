using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerControlManager : ControlManager
{
    public bool isPaused = false;
    public bool canMove = false;
    public Action pauseMake;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", player_direction_x);
        animator.SetFloat("Vertical", player_direction_y);
        animator.SetBool("Idling", playerIsIdle);

        if (!canMove)
            return;

        if (!isPaused)
        {
            Movement(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMake?.Invoke();
        }
    }

}
