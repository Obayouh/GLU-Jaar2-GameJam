using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject roomObject;
    GameObject lastRoom;
    public List<GameObject> rooms = new List<GameObject>();

    int roomAmount;
    int maxRoomAmount = 3;

    public Vector3 offset;

    public bool canAddNewRoom = true;

    public int score;

    CameraMovement cam;

    void Start()
    {
        cam = FindAnyObjectByType<CameraMovement>();
        canAddNewRoom = true;

        GameObject startRoom = Instantiate(roomObject);
        rooms.Add(startRoom);
        roomAmount++;
        lastRoom = startRoom;

        GenerateRoom();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canAddNewRoom)
        {
            score++;
            canAddNewRoom = false;
            cam.GoToNewRoom();
            GenerateRoom();
        }
    }

    private void GenerateRoom()
    {
        lastRoom.GetComponent<Room>().open = true;
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
