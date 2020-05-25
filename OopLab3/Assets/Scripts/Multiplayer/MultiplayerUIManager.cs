using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerUIManager : UI_Manager
{
    // Start is called before the first frame update

    public GameObject gamePanel;
   
    public Text InfoText;

    public Text Yorname;
    public Text EnemyName;


    public void SetInfoText(bool isWin, string enemyName ,bool enemyIsOff = false)
    {
        gamePanel.SetActive(false);
        ShowResult = () =>
         {
             if (isWin)
             {
                 winText.SetActive(true);
                 InfoText.text = "You win and " + enemyName + " loosed";
             }
             else
             {
                 looseText.SetActive(true);
                 InfoText.text = enemyName + "won and you loosed";
             }

             if (enemyIsOff)
             {
                 InfoText.text += '\n';
                 InfoText.text += "You win as your opponent(" + enemyName + ") has left the game";
             }
         };

        Result();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetNames(string name1, string name2)
    {
        Yorname.text = "Your name: " + name1;
        EnemyName.text = "Enemy name: " + name2;
        gamePanel.SetActive(true);
    }

    void Start()
    {
        gamePanel.SetActive(false);
        Init();
    }

    private void Update()
    {
        CheckKeyPressed();
    }
}
