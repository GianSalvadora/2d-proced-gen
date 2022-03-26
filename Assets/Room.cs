using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Room : MonoBehaviour
{

    public static Grid grid;
    public static RoomManager roomManager;

    public enum Direction
    {
        Left,
        Right,
        Down,
        NotPath
    }
    public enum Base
    {
        LR,
        LRT,
        LRB,
        LRTB,
        Random
    }
    public enum SubBase
    {
        Closed = 40,
        Trap = 30, 
        Mobs = 20,
        Treasure = 2
    }

    public SubBase subBase;
    public Base baseRoom;
    public Direction direction = Direction.NotPath;

    public TileGrid tileGrid;

    public void FirstRoom(Grid _grid, RoomManager _roomManager)
    {
        grid = _grid;
        roomManager = _roomManager;



        direction = Direction.Down;
        SetPath(Rand(1, 6), this);
    }

    public void SetPath(int dir, Room previousRoom)
    {
        grid.GetXY((Vector2)transform.position, out int currentX, out int currentY);
        roomManager.roomPath.Add(this);

        if (dir == 1 || dir == 2)
        {
            if (previousRoom.direction == Direction.Left || previousRoom.direction == Direction.Down)
            {
                direction = Direction.Left;
                Room nextRoom = grid.GetRoom(currentX-1, currentY);
                if (nextRoom != null)
                {
                    nextRoom.SetPath(Rand(1, 6), this);
                    return;
                }
                else if (nextRoom == null)
                {
                    roomManager.roomPath.Remove(this);
                    SetPath(5, previousRoom);
                    return;
                }
            }
            else if(previousRoom.direction == Direction.Right)
            {
                roomManager.roomPath.Remove(this);
                SetPath(3, previousRoom);
            }
        }
        else if (dir == 3 || dir == 4)
        {
            if (previousRoom.direction == Direction.Left)
            {
                roomManager.roomPath.Remove(this);
                SetPath(1, previousRoom);
            }
            else if (previousRoom.direction == Direction.Right || previousRoom.direction == Direction.Down)
            {
                direction = Direction.Right;
                Room nextRoom = grid.GetRoom(currentX + 1, currentY);
                if (nextRoom != null)
                {
                    nextRoom.SetPath(Rand(1, 6), this);
                }
                else if (nextRoom == null)
                {
                    roomManager.roomPath.Remove(this);
                    SetPath(5, previousRoom);
                }
            }
        }
        else if (dir == 5)
        {
            direction = Direction.Down;
            Room nextRoom = grid.GetRoom(currentX, currentY - 1);
            if (nextRoom != null)
            {
                nextRoom.SetPath(Rand(1, 6), this);
                return;
            }
            else if (nextRoom == null)
            {
                roomManager.OnPathMade();
            }
        }
    }

    public void FInishRoom()
    {
        grid.GetXY(transform.position, out int x, out int y);
        tileGrid = new TileGrid(8, 8, 1, grid.GetWorldPosition(x, y, false));

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(direction != Direction.NotPath)
        {
            sr.color = Color.white;
        }
        if(subBase == SubBase.Treasure)
        {
            sr.color = Color.yellow;
        }
        else if(subBase == SubBase.Closed)
        {
            sr.color = Color.black;
        }
        else if(subBase == SubBase.Mobs)
        {
            sr.color = Color.red;
        }
        else if(subBase == SubBase.Trap)
        {
            sr.color = Color.magenta;
        }

        
    }

    private int Rand(int inclusive, int exclusive)
    {
        return Random.Range(inclusive, exclusive);
    }
}
