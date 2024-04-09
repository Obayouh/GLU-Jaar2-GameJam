using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject roomObject;
    GameObject lastRoom;
    public List<GameObject> rooms = new List<GameObject>();

    int roomAmount;
    int maxRoomAmount = 2;

    void Start()
    {
        Instantiate(roomObject);
        lastRoom = roomObject;
        roomAmount++;

        GenerateRoom();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GenerateRoom();
        }
    }

    private void GenerateRoom()
    {
        GameObject newRoom = Instantiate(roomObject, (new Vector3(lastRoom.transform.position.x, lastRoom.transform.position.y, lastRoom.transform.position.z) + Vector3.forward), Quaternion.identity);
        rooms.Add(newRoom);
        lastRoom = newRoom;
        roomAmount++;

        if (roomAmount > maxRoomAmount)
        {
            RemoveRoom();
        }
    }

    private void RemoveRoom()
    {
        roomAmount--;
        Destroy(rooms[0].gameObject);
        rooms.RemoveAt(0);
    }
}
