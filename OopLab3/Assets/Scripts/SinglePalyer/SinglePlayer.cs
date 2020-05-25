using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeDll;
using System;

public class SinglePlayer : PlayerManager
{
    void Init(int start_x, int start_y, float current_x, float current_y, List<List<Point>> maze, FieldManager fieldManager)
    {
        base.start_x = start_x;
        base.start_y = start_y;
        base.maze = maze;
        base.fieldManager = fieldManager;

        palyer_current_x = current_x;
        palyer_current_y = current_y;
        Initial_door_count = 3;

        Vector3 startPlayerPos = fieldManager.wallMap.CellToWorld(new Vector3Int(start_x + 1, start_y + 1, 0));

        startPlayerPos += new Vector3(0.5f, 0.5f, 0);
        fieldManager.plm.gameObject.GetComponent<Transform>().position = startPlayerPos;
    }

    protected override bool CheckIfWin()
    {
        return count_doors_open == Initial_door_count;
    }

    public override void PlayerInit(FieldManager fieldManager)
    {
        Init(fieldManager.start_x, start_y, fieldManager.startPlayerPos.x, fieldManager.startPlayerPos.y, fieldManager.maze, fieldManager);
    }
    protected override bool NotifyOpenedDoor(Vector3Int coords, int i)
    {
        keys.Remove(keys[i]);
        fieldManager.decorMap.SetTile(coords, null);
        count_doors_open++;
        return true;
    }

    protected override void SetResults()
    {

        DateTime dateTime = DateTime.UtcNow.Date;
        ResultStatConfig.Date = dateTime.ToString("d");
        ResultStatConfig.Name = FieldConfig.Name;
        ResultStatConfig.Result = isWin ? "Win" : "Lose";
        ResultStatConfig.Steps = (steps / 40).ToString();
        ResultStatConfig.Time = spend_time.ToString();
        ResultStatConfig.Type = "SinglePlayer";

        StatisticHelper.WriteResult();
    }
}
