using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public event Action Completed;

    [Header("References")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private TMP_Text _helpText;

    [Header("Gameplay")]
    [SerializeField] private float _delay = 0.03f;
    [SerializeField] private float _endDelay = 0.2f;
    [Space(10)]
    [SerializeField] private string _helpTextInStage = "Нажмите Space чтобы пропустить";
    [SerializeField] private string _helpTextEndStage = "Нажмите Spce чтобы продолжить";
    [Space(10)]
    [Multiline(7)]
    [SerializeField] private String[] _texts;

    private bool _isDialogue;
    private bool _breakInsert;
    private int _currentTextIndex;

    private void Awake()
    {
        Completed += () => _panel.SetActive(false);
    }

    private void Start()
    {
        Next();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Next();
    }

    private void Next()
    {
        if (_panel.activeSelf == false)
            return;

        if (_isDialogue)
        {
            _breakInsert = true;
            return;
        }

        StartCoroutine(Insert());
    }


    private IEnumerator Insert()
    {
        var stringBuilder = new StringBuilder();
        var text = _texts[_currentTextIndex];

        _isDialogue = true;
        _helpText.text = _helpTextInStage;
        _dialogueText.text = "";
        _audioSource.Play();
        _breakInsert = false;

        for (int i = 0; i < text.Length; i++)
        {
            if (_breakInsert)
            {
                _dialogueText.text = text;
                break;
            }    

            stringBuilder.Append(text[i]);
            _dialogueText.text = stringBuilder.ToString();
            yield return new WaitForSeconds(_delay);
        }

        _audioSource.Stop();
        _isDialogue = false;
        _helpText.text = _helpTextEndStage;

        if (_currentTextIndex == _texts.Length-1)
        {
            yield return new WaitForSeconds(_endDelay);
            Completed?.Invoke();
        }
        else
            _currentTextIndex++;
    }
}
