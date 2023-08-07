using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartLevel : MonoBehaviour
{
    [SerializeField] private DialogueSystem _dialogueSystem;
    [SerializeField] private string _gameplaySceneName;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _dialogStartTime;
    [SerializeField] private float _endTime;

    private float _dialogTimer;
    private bool _isStart;

    private void Start()
    {
        _dialogueSystem.Completed += () => StartCoroutine(EndRoutine());

        _animator.SetTrigger("In");
    }

    private void Update()
    {
        DialogueTimer();
    }
    private void DialogueTimer()
    {
        if (_isStart)
            return;
        if (_dialogTimer >= _dialogStartTime)
        {
            _dialogueSystem.Next();
            _isStart = true;
        }
        else
            _dialogTimer += Time.deltaTime;
    }

    private IEnumerator EndRoutine()
    {
        _animator.SetTrigger("Out");
        yield return new WaitForSeconds(_endTime);
        SceneManager.LoadScene(2);
    }
}
