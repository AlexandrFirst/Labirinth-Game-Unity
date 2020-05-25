
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeDll;

[RequireComponent(typeof(SinglePlayerControlManager))]
public class SinglePlayerManager : SinglePlayer
{
    void Start()
    {
        keys = new List<char>();
       
        controlManager = GetComponent<SinglePlayerControlManager>();
        UIManager = FindObjectOfType<UIManager>();
        UIManager.ShowResult = ResultShow;

        (controlManager as SinglePlayerControlManager).InitDels(moveUp, moveDown, moveLeft, moveRight);
    }

    void Update()
    {
        GetComponent<Transform>().transform.position = new Vector3(palyer_current_x, palyer_current_y, -5);
        //Draw Player
        spend_time += Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
        UIManager.UpdateSteps(Mathf.Round(steps / 40).ToString());
        UIManager.UpdateTime(Mathf.Round(spend_time).ToString());

    }

    void ResultShow()
    {
        if (isWin)
        {
            UIManager.winText.SetActive(true);
            UIManager.looseText.SetActive(false);
        }
        else
        {
            UIManager.winText.SetActive(false);
            UIManager.looseText.SetActive(true);
        }

        //UIManager.r_time.text += Mathf.Round(spend_time).ToString();
        //UIManager.r_steps.text += Mathf.Round(steps / 40).ToString();

        SetResults();
    }
}
