using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerPage : LobbyManager
{
    // Start is called before the first frame update

    public InputField Name;
    public static string name;

    void Start()
    {
        if (!string.IsNullOrEmpty(SinglePlayerCustomevelPage.m_name))
            Name.text = SinglePlayerCustomevelPage.m_name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InputCheck()
    {
        name = Name.text;
        if (string.IsNullOrEmpty(name))
            FieldConfig.Name = "???";
        else
            FieldConfig.Name = name;
    }

    public void EasyCheck()
    {
        FieldConfig.Height = 10;
        FieldConfig.Width = 10;
    }
    public void MiddleCheck()
    {
        FieldConfig.Height = 15;
        FieldConfig.Width = 15;
    }
    public void HardCheck()
    {
        FieldConfig.Height = 20;
        FieldConfig.Width = 20;
    }



}
