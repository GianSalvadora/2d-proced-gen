using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    int height;
    int width;
    int[,] gridArray;

    public Grid(int _height, int _width)
    {
        height = _height;
        width = _width;

        gridArray = new int[width, height];
    }
}
