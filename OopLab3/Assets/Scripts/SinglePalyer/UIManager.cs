using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : UI_Manager
{
   

    // Start is called before the first frame update
    void Start()
    {
        Init();
        PauseGame = m_PauseGame;
    }


    private void Update()
    {
        CheckKeyPressed();

    }

    void m_PauseGame()
    {
        if (PauseButtonIsActive || warnPanelIsActive)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }

}
