using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{

    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _prefabSpell;

    private GameObject _player;
    private TurnManager _turnManager;
    private SpawnSlot _spawnSlot;
    private float _countdown;
    private float _resetTimer = 1.5f;

    void Start()
    {
        _player = FindObjectOfType<PlayerHealth>().gameObject;
        _turnManager = FindObjectOfType<TurnManager>();
        _spawnSlot = GetComponentInParent<SpawnSlot>();
        _countdown = _resetTimer;
    }

    void Update()
    {
        Transform parentPos = _spawnSlot.transform;

        if (_turnManager.FirstEnemyGo && parentPos.position.x == -3)
        {
            StartCountdown();
            if (_countdown <= 0f)
            {
                Instantiate(_prefabSpell, _shootPoint.position, _shootPoint.rotation);
                _countdown = _resetTimer;
                _turnManager.FirstEnemyGo = false;
                _turnManager.SecondEnemyGo = true;
            }
        }

        if (_turnManager.SecondEnemyGo && parentPos.position.x == 0)
        {
            StartCountdown();
            if (_countdown <= 0f)
            {
                Instantiate(_prefabSpell, _shootPoint.position, _shootPoint.rotation);
                _countdown = _resetTimer;
                _turnManager.SecondEnemyGo = false;
                _turnManager.LastEnemyGo = true;
            }
        }

        if (_turnManager.LastEnemyGo && parentPos.position.x == 3)
        {
            StartCountdown();
            if (_countdown <= 0f)
            {
                Instantiate(_prefabSpell, _shootPoint.position, _shootPoint.rotation);
                _countdown = _resetTimer;
                _turnManager.LastEnemyGo = false;
                _turnManager.PlayersTurn = true;
            }
        }
    }

    private void StartCountdown()
    {
        transform.LookAt(_player.transform);
        _countdown -= Time.deltaTime;
    }
}
