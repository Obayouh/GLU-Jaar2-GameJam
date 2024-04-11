using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform door;
    public bool open;

    void Update()
    {
        if (open)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        door.position += (Vector3.up * 5) * Time.deltaTime;
    }
}
