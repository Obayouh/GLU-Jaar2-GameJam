using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private int _currentRoomCheck;
    [SerializeField] private GameObject[] _rooms;

    private TurnManager _turnManager;
    private int _increaseRoomInt = 12;
    private int _randomNumber = 5;

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

        //for (int i = 0; i < _rooms.Length; i++)
        //{
        //    _rooms[i].SetActive(false);
        //}
        SetRoomFalse();

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

        if (_turnManager._floorNumber == 2 || _turnManager._floorNumber == 3 || _randomNumber == 0)
        {
            SetRoomFalse();
            _rooms[0].SetActive(true);
        }

        if (_turnManager._floorNumber == 4 || _turnManager._floorNumber == 5 || _randomNumber == 1)
        {
            SetRoomFalse();
            _rooms[1].SetActive(true);
            _currentRoomCheck = 1;
        }

        if (_turnManager._floorNumber == 6 || _turnManager._floorNumber == 7 || _randomNumber == 2)
        {
            SetRoomFalse();
            _rooms[2].SetActive(true);
            _currentRoomCheck = 2;
        }

        if (_turnManager._floorNumber == 8 || _turnManager._floorNumber == 9 || _randomNumber == 3)
        {
            SetRoomFalse();
            _rooms[3].SetActive(true);
            _currentRoomCheck = 3;
        }

        if (_turnManager._floorNumber == 10 || _turnManager._floorNumber == 11 || _randomNumber == 4)
        {
            SetRoomFalse();
            _rooms[4].SetActive(true);
            _currentRoomCheck = 4;
        }
    }

    private void SetRoomFalse()
    {
        for (int i = 0; i < _rooms.Length; i++)
        {
            _rooms[i].SetActive(false);
        }
    }
}
