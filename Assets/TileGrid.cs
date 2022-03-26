using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector2 origin;
    public Tile[,] tiles;

    public TileGrid(int _width, int _height, float _cellSize, Vector2 _origin)
    {
        width = _width;
        height = _height;
        cellSize = _cellSize;
        origin = _origin;

        tiles = new Tile[width, height];
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {

                if (y >= 1 && x >= 0)
                {
                    Debug.DrawLine(GetWorldPosition(x, y, false), GetWorldPosition(x + 1, y, false), Color.white, Mathf.Infinity);
                }
                if (x >= 1 && y >= 0)
                {
                    Debug.DrawLine(GetWorldPosition(x, y, false), GetWorldPosition(x, y + 1, false), Color.white, Mathf.Infinity);
                }
                
            }
        }
    }

    public Vector2 GetWorldPosition(int x, int y, bool center)
    {
        if (center)
        {
            return (new Vector2(x, y) * cellSize + origin) + (new Vector2(cellSize, cellSize) * .5f);
        }
        else
        {
            return (new Vector2(x, y) * cellSize + origin);
        }
    }

    public void GetXY(Vector2 position, out int x, out int y)
    {
        x = Mathf.FloorToInt((position - origin).x / cellSize);
        y = Mathf.FloorToInt((position - origin).y / cellSize); 
    }
}
