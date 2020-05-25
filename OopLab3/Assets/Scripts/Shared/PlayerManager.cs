using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeDll;

[RequireComponent(typeof(FieldManager))]
public abstract class PlayerManager : MonoBehaviour
{
    public float speed = 2f;
    public float hitLength = 0.3f;
    public float wideOffset = 0.1f;
    public float heightOffset = 0.1f;

    protected float palyer_current_x; //set off position of the eplayer due to the border
    protected float palyer_current_y;

    int real_size_y;
    int real_size_x;

    protected int start_x;//the actual position of the playe in the fireld
    protected int start_y;

    protected List<char> keys;
    protected List<List<Point>> maze;

    public int count_doors_open { get; protected set; }
    protected int Initial_door_count;

    public ControlManager controlManager;
    public FieldManager fieldManager;
    public UIManager UIManager;

    protected int steps;
    protected float spend_time;
    protected bool isWin = false;


    protected abstract bool CheckIfWin();
    public abstract void PlayerInit(FieldManager fieldManager);
    protected virtual void AssignPickedKey(char key) { }
    protected abstract void SetResults();
    protected virtual bool NotifyOpenedDoor(Vector3Int coords, int i)
    {
        return true;
    }

    protected bool moveUp(GameObject initiator)
    {
        if (isWin)
            return false;

        RaycastHit2D hitLeft = Physics2D.Raycast(initiator.transform.position + new Vector3(-wideOffset, 0, 0), Vector2.up, hitLength);
        RaycastHit2D hitRight = Physics2D.Raycast(initiator.transform.position + new Vector3(wideOffset, 0, 0), Vector2.up, hitLength);
        RaycastHit2D hitCenter = Physics2D.Raycast(initiator.transform.position, Vector2.up, hitLength);

        if (hitLeft.collider == null && hitRight.collider == null && hitCenter.collider == null)
        {
            steps++;
            palyer_current_y += speed * Time.deltaTime;
            return true;
        }
        else
        {
            Vector3 hitPos;
            if (hitLeft.collider != null)
                hitPos = hitLeft.point;
            else if (hitRight.collider != null)
                hitPos = hitRight.point;
            else
                hitPos = hitCenter.point;

            Debug.Log(fieldManager.decorMap.WorldToCell(hitPos));
            CheckAndPick_Key(fieldManager.decorMap.WorldToCell(hitPos));
        }




        return false;
    }
    protected bool moveDown(GameObject initiator)
    {
        if (isWin)
            return false;

        RaycastHit2D hitLeft = Physics2D.Raycast(initiator.transform.position + new Vector3(-wideOffset, 0, 0), -Vector2.up, hitLength);
        RaycastHit2D hitRight = Physics2D.Raycast(initiator.transform.position + new Vector3(wideOffset, 0, 0), -Vector2.up, hitLength);
        RaycastHit2D hitCenter = Physics2D.Raycast(initiator.transform.position, -Vector2.up, hitLength);


        if (hitLeft.collider == null && hitRight.collider == null && hitCenter.collider == null)
        {
            steps++;
            palyer_current_y -= speed * Time.deltaTime;
            return true;
        }
        else
        {
            Vector3 hitPos;
            if (hitLeft.collider != null)
                hitPos = hitLeft.point;
            else if (hitRight.collider != null)
                hitPos = hitRight.point;
            else
                hitPos = hitCenter.point;

            Debug.Log(fieldManager.decorMap.WorldToCell(hitPos));
            CheckAndPick_Key(fieldManager.decorMap.WorldToCell(hitPos));
        }
        return false;
    }
    protected bool moveLeft(GameObject initiator)
    {
        if (isWin)
            return false;

        RaycastHit2D hitUp = Physics2D.Raycast(initiator.transform.position + new Vector3(0, heightOffset, 0), -Vector2.right, hitLength);
        RaycastHit2D hitDown = Physics2D.Raycast(initiator.transform.position + new Vector3(0, -heightOffset, 0), -Vector2.right, hitLength);
        RaycastHit2D hitCenter = Physics2D.Raycast(initiator.transform.position, -Vector2.right, hitLength);



        if (hitUp.collider == null && hitDown.collider == null && hitCenter.collider == null)
        {
            steps++;
            palyer_current_x -= speed * Time.deltaTime;
            return true;
        }
        else
        {
            Vector3 hitPos;
            if (hitUp.collider != null)
                hitPos = hitUp.point;
            else if (hitDown.collider != null)
                hitPos = hitDown.point;
            else
                hitPos = hitCenter.point;

            Debug.Log(fieldManager.decorMap.WorldToCell(hitPos));
            CheckAndPick_Key(fieldManager.decorMap.WorldToCell(hitPos));
        }
        return false;
    }

    protected bool moveRight(GameObject initiator)
    {
        if (isWin)
            return false;

        RaycastHit2D hitUp = Physics2D.Raycast(initiator.transform.position + new Vector3(0, heightOffset, 0), Vector2.right, hitLength);
        RaycastHit2D hitDown = Physics2D.Raycast(initiator.transform.position + new Vector3(0, -heightOffset, 0), Vector2.right, hitLength);
        RaycastHit2D hitCenter = Physics2D.Raycast(initiator.transform.position, Vector2.right, hitLength);

        if (hitUp.collider == null && hitDown.collider == null && hitCenter.collider == null)
        {
            palyer_current_x += speed * Time.deltaTime;
            steps++;
            return true;
        }
        else
        {
            Vector3 hitPos;
            if (hitUp.collider != null)
                hitPos = hitUp.point;
            else if (hitDown.collider != null)
                hitPos = hitDown.point;
            else
                hitPos = hitCenter.point;


            CheckAndPick_Key(fieldManager.decorMap.WorldToCell(hitPos));
        }

        return false;
    }

    void CheckAndPick_Key(Vector3Int coords)
    {
        if (!(coords.y - 1 < maze.Count && coords.x - 1 < maze[0].Count))
            return;

        if (maze[coords.y - 1][coords.x - 1].door_key != ' ' && char.IsLower(maze[coords.y - 1][coords.x - 1].door_key))
        {
            // Debug.Log(maze[coords.y - 1][coords.x - 1].door_key);
            keys.Add(maze[coords.y - 1][coords.x - 1].door_key);
            AssignPickedKey(maze[coords.y - 1][coords.x - 1].door_key);
            fieldManager.decorMap.SetTile(coords, null);
        }
        else if (maze[coords.y - 1][coords.x - 1].door_key != ' ' && char.IsUpper(maze[coords.y - 1][coords.x - 1].door_key))
        {
            Debug.Log(maze[coords.y - 1][coords.x - 1].door_key);
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] == char.ToLower(maze[coords.y - 1][coords.x - 1].door_key))
                {
                    if (NotifyOpenedDoor(coords,i))
                        break;
                }
            }
        }

        if (CheckIfWin())
        {
            isWin = true;
            UIManager.Result();
        }
    }
}
