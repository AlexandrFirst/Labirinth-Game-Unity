using MazeDll;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class Multiplayer : PlayerManager
{
    public MultiplayerUIManager uiManager;
    protected TcpClient client;
    protected NetworkStream stream;

    protected BinaryWriter writer;
    protected BinaryReader reader;

    protected float enemy_current_x;
    protected float enemy_current_y;

    public GameObject player;
    public GameObject enemy;

    bool isHost;

    // protected List<char> player_keys;
    protected List<char> player_doors;
    protected List<Point> enemy_keys;
    protected List<Point> enemy_doors;
    List<char> OpenedDoors;

    protected char OpenedDoor;
    protected char PickedKey;

    protected Labr labr;

    int LabrSize;
    int real_size_x = 19;
    int real_size_y = 19;

    protected string yourName;
    protected string enemyName;



    protected int player_direction_x = 0;
    protected int player_direction_y = 0;

    protected int enemy_direction_x = 0;
    protected int enemy_direction_y = 0;

    protected int status = 0;
    protected bool enemyIsIdle = true;
    protected bool isLoose = false;
    protected bool EnemyIsOff = false;



    public void Init(TcpClient client, bool isHost)
    {
        this.client = client;
        this.isHost = isHost;
        stream = this.client.GetStream();
        writer = new BinaryWriter(stream);
        reader = new BinaryReader(stream);

        keys = new List<char>();
        player_doors = new List<char>();
        OpenedDoors = new List<char>();

        enemy_keys = new List<Point>();
        enemy_doors = new List<Point>();

        OpenedDoor = 'Q';
        PickedKey = 'q';

        real_size_x = MultiplayerConfig.Width;
        real_size_y = MultiplayerConfig.Height;
        
        if (isHost)
        {
            labr = new Labr();

            Debug.Log(real_size_x);
            Debug.Log(real_size_y);

            fieldManager.InitField(real_size_x, real_size_y, this);

            labr.maze = fieldManager.maze;
            labr.start_x = fieldManager.start_x;
            labr.start_y = fieldManager.start_y;

            byte[] array = ConvertToByteArray(labr);
            LabrSize = array.Length;
            Debug.Log(LabrSize);
            writer.Write(LabrSize);

            yourName = MultiplayerConfig.Name;

            writer.Write(array, 0, array.Length);

        }
        else
        {
            yourName = MultiplayerConfig.Name;

            LabrSize = reader.ReadInt32();
            Debug.Log(LabrSize);
            labr = GetLabr(LabrSize);
            real_size_x = labr.maze[0].Count;
            real_size_y = labr.maze.Count;
        }

        maze = labr.maze;

        palyer_current_x = labr.start_x + 1.5f;
        palyer_current_y = labr.start_y + 1.5f;

        enemy_current_x = labr.start_x + 1.5f;
        enemy_current_y = labr.start_y + 1.5f;

        fieldManager.DisplayMaze(labr.maze, real_size_x, real_size_y);

        if (yourName != null)
            writer.Write(yourName);

        enemyName = reader.ReadString();


        player.GetComponent<Transform>().position = new Vector3(palyer_current_x, palyer_current_y, 0);

        enemy.GetComponent<Transform>().position = new Vector3(enemy_current_x, enemy_current_y, 0);

        Thread thread = new Thread(new ThreadStart(receiveMessage));
        thread.Start();

        uiManager.SetNames(yourName, enemyName);

    }

    byte[] ConvertToByteArray(Labr labr)
    {
        byte[] data = Serializator.ObjectToByteArray(labr);
        return data;
    }

    Labr GetLabr(int LabrSize)
    {
        byte[] data = new byte[LabrSize];

        reader.Read(data, 0, LabrSize);

        Labr result = Serializator.ByteArrayToObject(data) as Labr;

        return result;
    }

    protected override bool CheckIfWin()
    {
        return OpenedDoors.Count == 3;
    }

    public override void PlayerInit(FieldManager fieldManager)
    {
        return;
    }
    protected override bool NotifyOpenedDoor(Vector3Int coords, int i)
    {
        OpenedDoor = labr.maze[coords.y - 1][coords.x - 1].door_key;
        fieldManager.decorMap.SetTile(coords, null);
        if (!OpenedDoors.Contains(OpenedDoor))
            OpenedDoors.Add(OpenedDoor);

        count_doors_open++;
        return true;
    }

    protected override void SetResults()
    {
        DateTime dateTime = DateTime.UtcNow.Date;
        ResultStatConfig.Date = dateTime.ToString("d");
        ResultStatConfig.Name = MultiplayerConfig.Name;
        ResultStatConfig.Opponet_name = enemyName;
        ResultStatConfig.Result = isWin ? "Win" : "Lose";
        ResultStatConfig.Steps = (steps / 40).ToString();
        ResultStatConfig.Time = spend_time.ToString();
        ResultStatConfig.Type = "Multiplayer";

        StatisticHelper.WriteResult();
    }

    void receiveMessage()
    {
        while (true)
        {
            if (reader != null)
            {


                status = reader.ReadInt32();

                if (status == 1)
                    isLoose = true;
                else if (status == 2)
                    EnemyIsOff = true;

                PickedKey = reader.ReadChar();
                OpenedDoor = reader.ReadChar();


                enemy_direction_x = reader.ReadInt32();
                enemy_direction_y = reader.ReadInt32();

                enemyIsIdle = reader.ReadBoolean();

                enemy_current_x = reader.ReadSingle();
                enemy_current_y = reader.ReadSingle();
            }
        }
    }

    protected override void AssignPickedKey(char key)
    {
        PickedKey = key;
    }
}
