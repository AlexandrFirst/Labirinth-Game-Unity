using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerControlManager : ControlManager
{
    int palyer_current_x;
    int palyer_current_y;

    Func<bool> CheckAndPick_Key;
   

    float horizontal;
    float vertiacal;

    public void InitDels( params Func<GameObject, bool>[] actions)
    {
        moveUp = actions[0];
        moveDown = actions[1];
        moveLeft = actions[2];
        moveRight = actions[3];
      

    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        horizontal = 0;
        vertiacal = -1;
        animator.SetBool("Idling", true);
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Horizontal", player_direction_x);
        animator.SetFloat("Vertical", player_direction_y);
        animator.SetBool("Idling", playerIsIdle);
        Movement(gameObject);
    }
}
