using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    public static RoomManager instance;


    private Room startRoom;
    [SerializeField] GameObject roomBase;
    [SerializeField] GameObject[] roomTypes;
    public List<Room> roomPath = new List<Room>();
    public Room[,] allRooms;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        Grid grid = new Grid(width, height, 8);
    }

    public Room LoadRoom()
    {
        Room temp = Instantiate(roomBase, transform.position, Quaternion.identity).GetComponent<Room>();
        return temp;
    }

    public void Initialize(Room[,] _rooms, Grid _grid)
    {
        allRooms = _rooms;
        List<Room> topRow = new List<Room>();
        for (int x = 0; x < _rooms.GetLength(0); x++)
        {
            for (int y = 0; y < _rooms.GetLength(1); y++)
            {
                if (y == _rooms.GetLength(1) - 1)
                {
                    topRow.Add(_rooms[x, y]);
                }
            }
        }

        int RandNum = Random.Range(0, topRow.Count);
        startRoom = topRow[RandNum];
        startRoom.FirstRoom(_grid, this);
    }

    public void OnPathMade()//lr - lrt - lrb - lrtb
    {
        for (int i = 0; i < roomPath.Count; i++)
        {

            Room currentRoom = GetPath(i);
            Room prevRoom = GetPath(i - 1);
            Transform trans = null;
            if (prevRoom != null && i != roomPath.Count - 1)//not start and not end
            {
                if (currentRoom.direction == Room.Direction.Down)
                {
                    if (prevRoom.direction == Room.Direction.Down)
                    {
                        currentRoom.baseRoom = Room.Base.LRTB;
                        trans = Instantiate(roomTypes[3], currentRoom.transform.position, Quaternion.identity).transform;
                    }
                    else
                    {
                        currentRoom.baseRoom = Room.Base.LRB;
                        trans = Instantiate(roomTypes[2], currentRoom.transform.position, Quaternion.identity).transform;
                    }
                }
                else
                {
                    if (prevRoom.direction == Room.Direction.Down)
                    {
                        currentRoom.baseRoom = Room.Base.LRT;
                        trans = Instantiate(roomTypes[1], currentRoom.transform.position, Quaternion.identity).transform;
                    }
                    else
                    {
                        currentRoom.baseRoom = Room.Base.LR;
                        trans = Instantiate(roomTypes[0], currentRoom.transform.position, Quaternion.identity).transform;
                    }
                }
            }
            else if (prevRoom == null && i != roomPath.Count - 1)//is start
            {
                if (currentRoom.direction == Room.Direction.Down)
                {
                    currentRoom.baseRoom = Room.Base.LRB;
                    trans = Instantiate(roomTypes[2], currentRoom.transform.position, Quaternion.identity).transform;
                }
                else
                {
                    currentRoom.baseRoom = Room.Base.LR;
                    trans = Instantiate(roomTypes[0], currentRoom.transform.position, Quaternion.identity).transform;
                }
            }
            else if(i == roomPath.Count - 1)
            {
                if (prevRoom.direction == Room.Direction.Down)
                {
                    currentRoom.baseRoom = Room.Base.LRT;
                    trans = Instantiate(roomTypes[1], currentRoom.transform.position, Quaternion.identity).transform;
                }
                else
                {
                    currentRoom.baseRoom = Room.Base.LR;
                    trans = Instantiate(roomTypes[0], currentRoom.transform.position, Quaternion.identity).transform;
                }
            }

            List<Transform> allChild = new List<Transform>();
            foreach (Transform child in trans)
            {
                allChild.Add(child);
            }
            foreach(Transform child in allChild)
            {
                child.parent = currentRoom.transform;
            }

            Destroy(trans.gameObject);
        }

        foreach(Room r in allRooms)
        {
            if(r.direction == Room.Direction.NotPath)
            {
                System.Array choices = System.Enum.GetValues(typeof(Room.SubBase));
                List<Room.SubBase> type = new List<Room.SubBase>();
                foreach(Room.SubBase i in choices)
                {
                    int amt = (int)i;
                    for (int x = 0; x <= amt ; x++)
                    {
                        type.Add(i);
                    }
                }

                r.subBase = type[Random.Range(0, type.Count)];
                r.FInishRoom();
            }
            else
            {
                r.FInishRoom();
            }
        }
    }
    private Room GetPath(int pathIndex)
    {
        if (pathIndex < 0)
        {
            return null;
        }
        else if (pathIndex >= roomPath.Count)
        {
            return null;
        }
        else
        {
            return roomPath[pathIndex];
        }
    }
}
