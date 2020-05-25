using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerCustomevelPage : LobbyManager
{
    // Start is called before the first frame update

    public Image WidthCross;
    public Image HeightCross;


    public InputField width;
    public InputField height;
    public InputField name;

    int m_width = 0;
    int m_height = 0;
    public static string m_name;

    void Start()
    {
        WidthCross.enabled = false;
        HeightCross.enabled = false;


        if (!string.IsNullOrEmpty(SinglePlayerPage.name))
            name.text = SinglePlayerPage.name;

    }

    public void StartEnterParams()
    {
        WidthCross.enabled = false;
        HeightCross.enabled = false;
    }

    public void ParamsCheck()
    {
        if(!string.IsNullOrEmpty(width.text))
            m_width = int.Parse(width.text);

        if (!string.IsNullOrEmpty(height.text))
            m_height = int.Parse(height.text);
        if(!string.IsNullOrEmpty(name.text))
            m_name = name.text;

        Debug.Log(m_width + " - width");
        Debug.Log(m_height + " - height");
    }

    public void CheckInputs()
    {
       
        bool b = true;
        if ((m_width < 10 || m_width > 20) || (m_height < 10 || m_height > 20))
        {
            if (m_width < 10 || m_width > 20)
                WidthCross.enabled = true;
            if (m_height < 10 || m_height > 20)
                HeightCross.enabled = true;

            b = false;
        }

        if (string.IsNullOrEmpty(m_name))
            FieldConfig.Name = "???";
        else
            FieldConfig.Name = m_name;

        if (b)
        {
            FieldConfig.Width = m_width;
            FieldConfig.Height = m_height;
            LoadGame(1);
        }
    }

}
