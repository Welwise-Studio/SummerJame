using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<UnitBehaviour> _enemies;
    [SerializeField] private Door _entryDoor;
    [SerializeField] private Door _exitDoor;

    private List<UnitBehaviour> _activeEnemies;

    private void Awake()
    {
        _activeEnemies = new List<UnitBehaviour>();
        _enemies.ForEach(item => _activeEnemies.Add(item));
        _enemies.ForEach(item => item.OnDie += () => RemoveEnemyFromList(item));
    }

    private void RemoveEnemyFromList(UnitBehaviour enemy)
    {
        _activeEnemies.Remove(enemy);

        if(_activeEnemies.Count == 0)
            OpenExitDoor();
    }

    private void OpenExitDoor()
    {
        _exitDoor.Open();
    }

    private void CloseEntryDoor()
    {

    }
}
