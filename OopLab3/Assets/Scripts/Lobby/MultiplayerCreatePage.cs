using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerCreatePage : LobbyManager
{
    public Image WidthCrossImg;
    public Image HeightCrossImg;
    public Image NameImg;


    public InputField width;
    public InputField height;
    public InputField name;

    int m_width = 0;
    int m_height = 0;
    string m_name;

    void Start()
    {
        WidthCrossImg.enabled = false;
        HeightCrossImg.enabled = false;
        NameImg.enabled = false;
    }

    public void StartEnterParams()
    {
        WidthCrossImg.enabled = false;
        HeightCrossImg.enabled = false;
        NameImg.enabled = false;
    }

    public void ParamsCheck()
    {
        if (!string.IsNullOrEmpty(width.text))
            m_width = int.Parse(width.text);

        if (!string.IsNullOrEmpty(height.text))
            m_height = int.Parse(height.text);
        if (!string.IsNullOrEmpty(name.text))
            m_name = name.text;

    }

    public void CheckInputs()
    {

        bool b = true;
        if (m_width < 10 || m_width > 20 || m_height < 10 || m_height > 20 || string.IsNullOrEmpty(m_name))
        {
            if (m_width < 10 || m_width > 20)
                WidthCrossImg.enabled = true;
            if (m_height < 10 || m_height > 20)
                HeightCrossImg.enabled = true;
            if (string.IsNullOrEmpty(m_name))
                NameImg.enabled = true;

            b = false;
        }

        if (b)
        {
            MultiplayerConfig.Width = m_width;
            MultiplayerConfig.Height = m_height;
            MultiplayerConfig.Name = m_name;
            MultiplayerConfig.IsHost = true;
            LoadGame(2);
        }
    }

}
