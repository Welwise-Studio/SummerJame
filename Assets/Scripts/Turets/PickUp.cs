using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private float _distToPickUp;
    [SerializeField] private GameObject _button;
    
    private Player _player;
    private Vector3 _playerPos;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>();
        _playerPos = _player.transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _playerPos) <= _distToPickUp )
        {
            _button.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
            }
        }else
        {
            _button.SetActive(false);
        }
    }
}
