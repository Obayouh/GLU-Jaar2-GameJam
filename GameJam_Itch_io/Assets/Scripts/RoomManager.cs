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

    public Vector3 offset;

    public bool canAddNewRoom = true;

    CameraMovement cam;

    void Start()
    {
        cam = FindAnyObjectByType<CameraMovement>();
        canAddNewRoom = true;

        GameObject startRoom = Instantiate(roomObject);
        rooms.Add(startRoom);
        lastRoom = roomObject;
        roomAmount++;

        GenerateRoom();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canAddNewRoom)
        {
            canAddNewRoom = false;
            cam.GoToNewRoom();
            lastRoom.GetComponent<Room>().open = true;
            GenerateRoom();
        }
    }

    private void GenerateRoom()
    {
        GameObject newRoom = Instantiate(roomObject, (new Vector3(lastRoom.transform.position.x, lastRoom.transform.position.y, lastRoom.transform.position.z) + offset), Quaternion.identity);
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
