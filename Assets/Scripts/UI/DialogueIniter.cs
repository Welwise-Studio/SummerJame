using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIniter : MonoBehaviour
{
    [SerializeField] private DialogueSystem _dialogue;
    [SerializeField] private GameObject _dialoguePnael;
    private bool _isInit;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isInit && other.GetComponentInParent<Player>() != null)
        {
            _dialogue.enabled = true;
            _dialoguePnael.SetActive(true);
            _dialogue.Next();
            _isInit = true;
        }
    }
}
