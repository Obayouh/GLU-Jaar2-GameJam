using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _prefabSpell;

    private TurnManager _turnManager;
    private CurrentEnemies _currentEnemies;
    private int _indexPosition;
    private float _countdown;
    private float _resetTimer = 1.5f;

    void Start()
    {
        _currentEnemies = GetComponentInParent<CurrentEnemies>();
        _turnManager = FindObjectOfType<TurnManager>();
        _countdown = _resetTimer;
        _indexPosition = _currentEnemies._spawnedEnemies.FindIndex(c => c.name.Equals(this.gameObject.name));
    }

    void Update()
    {
        if (_turnManager.FirstEnemyGo && _indexPosition == 0)
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

        if (_turnManager.SecondEnemyGo && _indexPosition == 1)
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

        if (_turnManager.LastEnemyGo && _indexPosition == 2)
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
