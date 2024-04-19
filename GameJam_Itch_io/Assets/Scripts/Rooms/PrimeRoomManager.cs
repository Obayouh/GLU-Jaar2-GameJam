using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeRoomManager : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab; // Prefab of the room to be generated
    [SerializeField] private int maxRooms = 3; // Maximum number of rooms to be loaded at a time
    [SerializeField] private GameObject staircasePrefab; // Prefab for the staircase

    private List<GameObject> currentRooms = new(); // List to store currently loaded rooms

    private void Start()
    {
        GenerateRooms();
    }

    private void GenerateRooms()
    {
        for (int i = 0; i < maxRooms; i++)
        {
            Vector3 spawnPosition = Vector3.zero; // Default spawn position for the first room

            if (currentRooms.Count > 0)
            {
                // If there are already rooms, calculate spawn position above the last room
                float roomHeight = roomPrefab.transform.localScale.y * 10f;
                Vector3 lastRoomPosition = currentRooms[currentRooms.Count - 1].transform.position;
                spawnPosition = lastRoomPosition + new Vector3(0, roomHeight, 0);
            }

            GameObject room = Instantiate(roomPrefab, spawnPosition, Quaternion.identity);
            currentRooms.Add(room);

            if (currentRooms.Count > 1)
            {
                // If there are more than one room, spawn a staircase to connect them
                GameObject staircase = Instantiate(staircasePrefab, spawnPosition, Quaternion.identity);
                ConnectRooms(currentRooms[currentRooms.Count - 2], room, staircase);
            }

            if (currentRooms.Count > maxRooms)
            {
                // Unload the oldest room and remove it from the list
                GameObject roomToUnload = currentRooms[0];
                currentRooms.RemoveAt(0);
                Destroy(roomToUnload);
            }
        }
    }

    private void ConnectRooms(GameObject roomA, GameObject roomB, GameObject staircase)
    {
        // Calculate position for the staircase between the rooms
        Vector3 midpoint = (roomA.transform.position + roomB.transform.position) / 2f;
        staircase.transform.position = midpoint;

        // Align the staircase with the rooms
        Vector3 direction = roomB.transform.position - roomA.transform.position;
        staircase.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
