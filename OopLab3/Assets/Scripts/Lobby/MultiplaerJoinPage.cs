using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class MultiplaerJoinPage : LobbyManager
{

    public Image NameImg;
    public Image IpImg;

    public InputField name;
    public InputField ip;

    string m_ip;
    string m_name;

    void Start()
    {
        NameImg.enabled = false;
        NameImg.enabled = false;
    }

    public void StartEnterParams()
    {
        NameImg.enabled = false;
        NameImg.enabled = false;
    }

    public void ParamsCheck()
    {

        if (!string.IsNullOrEmpty(name.text))
            m_name = name.text;

        if (!string.IsNullOrEmpty(ip.text))
            m_ip = ip.text;
    }

    public void CheckInputs()
    {

        bool b = true;
        if (string.IsNullOrEmpty(m_name) || string.IsNullOrEmpty(m_ip))
        {
            if (string.IsNullOrEmpty(m_name))
                NameImg.enabled = true;
            if (string.IsNullOrEmpty(m_ip))
                IpImg.enabled = true;

            b = false;
        }
        if (!string.IsNullOrEmpty(m_ip))
        {
            string Pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";

            Regex check = new Regex(Pattern);

            b = check.IsMatch(m_ip, 0);

        }

        if (b)
        {
            MultiplayerConfig.IP = m_ip;
            MultiplayerConfig.Name = m_name;
            MultiplayerConfig.IsHost = false;
            LoadGame(2);
        }
    }
}

