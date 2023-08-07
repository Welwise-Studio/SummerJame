using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<UnitBehaviour> _enemies;
    [SerializeField] private Door _entryDoor;
    [SerializeField] private Door _exitDoor;

    private void Update()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy!=null)
            {
                return;
            }
        }
        OpenExitDoor();
    }

    private void OpenExitDoor()
    {
        _exitDoor.Open();
    }


}
