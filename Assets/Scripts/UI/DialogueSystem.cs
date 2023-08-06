using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _panel;
    [SerializeField] private float _delayBase = 0.03f;
    [SerializeField] private TMP_Text _text;

    private bool _isDialogue;
    private float _currentDelay;

    private void Awake()
    {
        Show(_text.text);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Skip();
        }
    }

    public void Skip()
    {
        if (!_isDialogue)
            return;

        _currentDelay = _delayBase / 3;
    }

    public void Show(string text)
    {
        _panel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Insert(text));
    }

    public void Hide()
    {
        _panel.SetActive(false);
        StopAllCoroutines();
    }


    private IEnumerator Insert(string text)
    {
        _currentDelay = _delayBase;
        _isDialogue = true;
        _text.text = "";
        _audioSource.Play();
        var stringBuilder = new StringBuilder();
        for (int i = 0; i < text.Length; i++)
        {
            _audioSource.pitch = (_delayBase / _currentDelay) * .8f;
            stringBuilder.Append(text[i]);
            if (_text.isTextOverflowing)
                stringBuilder.Remove(0, stringBuilder.Length-2);

            _text.text = stringBuilder.ToString();
            yield return new WaitForSeconds(_currentDelay);
        }
        _audioSource.Stop();
        _isDialogue = false;
    }
}
