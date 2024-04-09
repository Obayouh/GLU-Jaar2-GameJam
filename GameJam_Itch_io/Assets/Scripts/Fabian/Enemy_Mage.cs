using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mage : MonoBehaviour
{
    [SerializeField] private TurnManager _turnManager;
    [SerializeField] private GameObject _player;

    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _prefabSpell;

    private CurrentEnemies _currentEnemies;
    [SerializeField] private int _indexPosition;

    void Start()
    {
        _currentEnemies = GetComponentInParent<CurrentEnemies>();
        _indexPosition = _currentEnemies._spawnedEnemies.FindIndex(c => c.name.Equals(this.gameObject.name));
    }

    void Update()
    {
        if (_turnManager.FirstEnemyGo && _indexPosition == 0)
        {
            transform.LookAt(_player.transform);
            Instantiate(_prefabSpell, _shootPoint.position, _shootPoint.rotation);
            _turnManager.FirstEnemyGo = false;
            _turnManager.SecondEnemyGo = true;
        }

        if (_turnManager.SecondEnemyGo && _indexPosition == 1)
        {
            transform.LookAt(_player.transform);
            Instantiate(_prefabSpell, _shootPoint.position, _shootPoint.rotation);
            _turnManager.SecondEnemyGo = false;
            _turnManager.LastEnemyGo = true;
        }

        if (_turnManager.LastEnemyGo && _indexPosition == 2)
        {
            transform.LookAt(_player.transform);
            Instantiate(_prefabSpell, _shootPoint.position, _shootPoint.rotation);
            _turnManager.LastEnemyGo = false;
            _turnManager.PlayersTurn = true;
        }
    }
}
