using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private int _currentRoomCheck;
    [SerializeField] private GameObject[] _rooms;

    private TurnManager _turnManager;
    private int _increaseRoomInt = 12;
    private int _randomNumber;

    void Start()
    {
        _turnManager = FindFirstObjectByType<TurnManager>();
        if (_rooms.Length == 0)
        {
            return;
        }

        _rooms[0].SetActive(true);
        _rooms[1].SetActive(false);
        _rooms[2].SetActive(false);
        _rooms[3].SetActive(false);
        _rooms[4].SetActive(false);
        _currentRoomCheck = 0;
    }

    public void ChangeRoom()
    {
        if (_rooms.Length == 0)
        {
            return;
        }

        for (int i = 0; i < _rooms.Length; i++)
        {
            _rooms[i].SetActive(false);
        }

        if (_turnManager._floorNumber == _increaseRoomInt)
        {
            int randoNumber = Random.Range(0, 5);
            Debug.Log("1_" + randoNumber);
            if (randoNumber == _currentRoomCheck)
            {
                randoNumber = Random.Range(0, 5);
                Debug.Log("2_" + randoNumber);
            }
            else
            {
                _randomNumber = randoNumber;
            }

            _increaseRoomInt += 2;
        }

        if (_randomNumber == 0)
        {
            _rooms[0].SetActive(true);
        }

        else if (_turnManager._floorNumber == 4 || _randomNumber == 1)
        {
            _rooms[1].SetActive(true);
            _currentRoomCheck = 1;
        }

        else if (_turnManager._floorNumber == 6 || _randomNumber == 2)
        {
            _rooms[2].SetActive(true);
            _currentRoomCheck = 2;
        }

        else if (_turnManager._floorNumber == 8 || _randomNumber == 3)
        {
            _rooms[3].SetActive(true);
            _currentRoomCheck = 3;
        }

        else if (_turnManager._floorNumber == 10 || _randomNumber == 4)
        {
            _rooms[4].SetActive(true);
            _currentRoomCheck = 4;
        }
    }
}
