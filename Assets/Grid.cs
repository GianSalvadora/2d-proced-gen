using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{

    private int width;
    private int height;
    private float cellSize;
    public Room[,] rooms;

    public Grid(int _width, int _height, float _cellSize)
    {
        width = _width;
        height = _height;
        cellSize = _cellSize;

        rooms = new Room[width, height];
       
        for (int x = 0; x < rooms.GetLength(0); x++)
        {
            for (int y = 0; y < rooms.GetLength(1); y++)
            {
                Debug.DrawLine(GetWorldPosition(x,y,false), GetWorldPosition(x + 1, y, false), Color.black, Mathf.Infinity);
                Debug.DrawLine(GetWorldPosition(x, y, false), GetWorldPosition(x, y + 1, false), Color.black, Mathf.Infinity);
                if(rooms[x,y] == null)
                {
                    rooms[x, y] = RoomManager.instance.LoadRoom();
                    rooms[x, y].transform.position = GetWorldPosition(x, y, true);
                }
            }
        }
        RoomManager.instance.Initialize(rooms, this);
    }

    public Vector2 GetWorldPosition(int x, int y, bool center)
    {
        if (center)
        {
            return (new Vector2(x, y) * cellSize) + (new Vector2(cellSize, cellSize) * .5f);
        }
        else
        {
            return (new Vector2(x, y) * cellSize);
        }
    }

    public void GetXY(Vector2 position, out int x, out int y)
    {
        x = Mathf.FloorToInt(position.x / cellSize);
        y = Mathf.FloorToInt(position.y / cellSize);
    }
    public Room GetRoom(int x, int y)
    {

        if(x < 0 || x >= width)
        {
            return null;
        }
        else if(y < 0 || y >= height)
        {
            return null;
        }
        else
        {
            return rooms[x, y];
        }
    }

    public void SetRoom(Room room, int x, int y)
    {
        rooms[x, y] = room;   
    }
}
