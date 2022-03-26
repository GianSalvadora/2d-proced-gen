using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum tileOds
    {
        Persistent,
        Random,
    }

    public enum tileType
    {
        Block,
        Trap,
        Mob
    }

    public tileOds ods = tileOds.Random;
    public tileType type = tileType.Block;
    

}
