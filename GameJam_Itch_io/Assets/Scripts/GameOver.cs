using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    RoomManager roomManager;

    private void OnEnable()
    {
        roomManager = FindAnyObjectByType<RoomManager>();

        if (roomManager.score == 1)
        {
            scoreText.text = "You Cleared " + roomManager.score + " Room";
        }
        else
        {
            scoreText.text = "You Cleared " + roomManager.score + " Rooms";
        }
    }
}
