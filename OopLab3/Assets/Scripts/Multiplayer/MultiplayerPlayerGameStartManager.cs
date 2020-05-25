using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerPlayerGameStartManager : MonoBehaviour
{
    // Start is called before the first frame update


    public MultiplayerManager controlmanager;
 
    string ip = "192.168.1.104";

    TcpListener listener;
    TcpClient client;
    NetworkStream stream;

    BinaryWriter writer;
    BinaryReader reader;

    bool startGame = false;


    bool isHost;

    public GameObject TerminatePanel;


    public Text infoText;
    public Text errorText;

    Thread listening;
    Thread speaking;

    bool b = true;

    bool keyPresseed = false;
    bool QPressed = false;
    bool ErrorHappened = false;

    void Start()
    {
        listener = null;
        TerminatePanel.SetActive(false);
        if (MultiplayerConfig.IsHost)
        {

            ip = GetIpAdress();
            SetHost();
        }
        else
        {
            ip = MultiplayerConfig.IP;
            SetClient();
        }

    }

    string GetIpAdress()
    {
        String strHostName = string.Empty;
        strHostName = Dns.GetHostName();
        IPHostEntry ipHostEntry = Dns.GetHostEntry(strHostName);
        IPAddress[] address = ipHostEntry.AddressList;
        string local_ip = address[1].ToString();

        return local_ip;
    }

    void SetHost()
    {
        isHost = true;
        Debug.Log(ip);
        infoText.text = "Waiting for the player. To return to the menu, press according button o press Q button on thr keyboard \n The Ip is " + ip;
        InitServer_Client();
    }
    void SetClient()
    {
        isHost = false;
        Debug.Log(ip);
        infoText.text = "Trying to link to the client by ip:" + ip + "and by port: 8888";
        InitServer_Client();
    }
    bool initSucess = true;

    void InitServer_Client()
    {
        
      
        TerminatePanel.SetActive(true);

        Thread thread = new Thread(() =>
        {
            initSucess = GameInit();
            startGame = initSucess;
            if (initSucess)
            {
                Debug.Log("JJJJRRRRR");
            }
            else
            {
                if (isHost)
                {
                    
                    TcpClient client_temp = new TcpClient(ip, 8888);

                    if (stream != null)
                        stream = null;
                    if (client_temp != null)
                        client_temp.Close();
                    if (client != null)
                        client.Close();

                    listener.Stop();
                }


            }
        });
        thread.Start();
    }

    bool GameInit()
    {
        if (isHost)
        {
            Debug.Log("waiting for the player");


            listener = new TcpListener(IPAddress.Any, 8888);
            listener.Start();

            Thread acceptor = new Thread(() => { client = listener.AcceptTcpClient(); });
            acceptor.Start();

            while (client == null)
            {
                b = false;
                while (!keyPresseed)
                {
                    if (client != null)
                    {
                        b = true;
                        break;
                    }
                }
                if (b)
                    break;

                if (QPressed)
                {
                    return false;
                }

            }

            Debug.Log("Player appeared");

            GetStream(client);
        }
        else
        {
            bool accept = true;
            Thread acceptor = new Thread(() =>
            {
                try
                {
                    client = new TcpClient(ip, 8888);
                    accept = true;
                }
                catch
                {
                   
                    ErrorHappened = true;
                    accept = false;
                }
            });
            acceptor.Start();

            while (client == null)
            {
                b = false;
                while (!keyPresseed)
                {
                    if (!accept)
                    {
                        return false;
                    }

                    if (client != null)
                    {
                        b = true;
                        break;
                    }
                }
                if (b)
                    break;
                if (QPressed)
                {
                    return false;
                }
            }
            GetStream(client);
        }

        return true;
    }

    void GetStream(TcpClient client)
    {
        stream = client.GetStream();
        reader = new BinaryReader(stream);
        writer = new BinaryWriter(stream);
    }

    public void Update()
    {
        QPressed = false;
        keyPresseed = false;

        if (Input.anyKey)
            keyPresseed = true;
        if (Input.GetKey(KeyCode.Q))
            QPressed = true;

        if (!initSucess)
        {
            if (!ErrorHappened)
            {
                SceneManager.LoadScene(0);
            }
            else
                errorText.text = "There is no such ip";

        }
        if (startGame)
        {
           
            controlmanager.Init(client,isHost);

           
            TerminatePanel.SetActive(false);
            Destroy(gameObject);
        }

    }

    public void BackButtonPress()
    {
        if (!ErrorHappened)
        {
            keyPresseed = true;
            QPressed = true;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
