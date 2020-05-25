using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class UI_Manager : MonoBehaviour
{
    public Animator resultPanelAnimator;

    public Action CameraChange;
    public Action MusicVolumeChange;
    public Action ShowResult;
    public Action PauseGame;

    public float minZoom = 3f;
    public float maxZoom = 10f;


    public GameObject winText;
    public GameObject looseText;

    public Text steps;
    public Text time;

    public Text r_steps;
    public Text r_time;

    public Slider MusicVolume;
    public Slider ZoomLevel;

    public GameObject ResultPanel;
    public GameObject PausePanel;
    public GameObject WarnPanel;

    protected bool PauseButtonIsActive = false;
    protected bool warnPanelIsActive = false;

    

    public void UpdateSteps(string steps)
    {
        this.steps.text = "Steps: " + steps;
    }

    public void UpdateTime(string time)
    {
        this.time.text = "Time: " + time;
    }

    // Start is called before the first frame update
    public void Init()
    {
        PauseButtonIsActive = false;
        PausePanel.SetActive(false);

        WarnPanel.SetActive(false);
        ResultPanel.SetActive(false);

        ZoomLevel.minValue = minZoom;
        ZoomLevel.maxValue = maxZoom;

        winText.SetActive(false);
        looseText.SetActive(false);
    }

    public void ChangeZoom()
    {
        CameraChange?.Invoke();
    }
    public void MusicChange()
    {
        MusicVolumeChange?.Invoke();
    }

    public void CheckKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (warnPanelIsActive)
                ActivateWarnPanel();
            else
                ActivatePausePanel();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateWarnPanel();
        }
    }

    public void ActivatePausePanel()
    {
        PausePanel.SetActive(!PauseButtonIsActive);

        PauseButtonIsActive = !PauseButtonIsActive;

        PauseGame?.Invoke();
    }

    public void ActivateWarnPanel()
    {
        WarnPanel.SetActive(!warnPanelIsActive);
        warnPanelIsActive = !warnPanelIsActive;

        PauseGame?.Invoke();
    }

    public void Result()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        ResultPanel.SetActive(true);

        r_steps.text = steps.text;
        r_time.text = time.text;

        if (ShowResult != null)
            ShowResult();

        resultPanelAnimator.SetBool("GameEnd", true);

    }

    public void LobbyLoad()
    {
        SceneManager.LoadScene(0);
    }
}
