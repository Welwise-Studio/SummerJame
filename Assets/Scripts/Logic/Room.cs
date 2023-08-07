using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] public List<UnitBehaviour> _enemies;
    [SerializeField] private Door _entryDoor;
    [SerializeField] private Door _exitDoor;
    [SerializeField] private Room _nextRoom = null;

    private void Update()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy != null)
            {
                return;
            }
        }
        OpenExitDoor();
    }

    private void OpenExitDoor()
    {
        if (_nextRoom != null)
        {
            foreach (var enemy in _nextRoom._enemies)
            {
                if (enemy != null)
                {
                    enemy.GetComponent<Turret>().CanActivate = true;
                }
            }
        }
        _exitDoor.Open();
    }
}
