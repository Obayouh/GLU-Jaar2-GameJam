using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private int _currentRoomCheck;
    [SerializeField] private GameObject[] _rooms;

    private TurnManager _turnManager;
    private int _increaseRoomInt = 11;
    private int _randomNumber;

    void Start()
    {
        _turnManager = FindFirstObjectByType<TurnManager>();
        _rooms[0].SetActive(true);
        _rooms[1].SetActive(false);
        _rooms[2].SetActive(false);
        _rooms[3].SetActive(false);
        _rooms[4].SetActive(false);
        _currentRoomCheck = 0;
    }

    void Update()
    {
        if (_randomNumber == 0)
        {
            _rooms[0].SetActive(true);
            _rooms[1].SetActive(false);
            _rooms[2].SetActive(false);
            _rooms[3].SetActive(false);
            _rooms[4].SetActive(false);
        }

        if (_turnManager._floorNumber == 3 || _randomNumber == 1)
        {
            _rooms[0].SetActive(false);
            _rooms[1].SetActive(true);
            _rooms[2].SetActive(false);
            _rooms[3].SetActive(false);
            _rooms[4].SetActive(false);
            _currentRoomCheck = 1;
        }

        if (_turnManager._floorNumber == 5 || _randomNumber == 2)
        {
            _rooms[0].SetActive(false);
            _rooms[1].SetActive(false);
            _rooms[2].SetActive(true);
            _rooms[3].SetActive(false);
            _rooms[4].SetActive(false);
            _currentRoomCheck = 2;
        }

        if (_turnManager._floorNumber == 7 || _randomNumber == 3)
        {
            _rooms[0].SetActive(false);
            _rooms[1].SetActive(false);
            _rooms[2].SetActive(false);
            _rooms[3].SetActive(true);
            _rooms[4].SetActive(false);
            _currentRoomCheck = 3;
        }

        if (_turnManager._floorNumber == 9 || _randomNumber == 4)
        {
            _rooms[0].SetActive(false);
            _rooms[1].SetActive(false);
            _rooms[2].SetActive(false);
            _rooms[3].SetActive(false);
            _rooms[4].SetActive(true);
            _currentRoomCheck = 4;
        }

        if (_turnManager._floorNumber == _increaseRoomInt)
        {
            int randoNumber = Random.Range(0, 5);
            if (randoNumber == _currentRoomCheck)
            {
                randoNumber = Random.Range(0, 5);
            }
            else
            {
                _randomNumber = randoNumber;
            }

            _increaseRoomInt += 2;
        }
    }
}
