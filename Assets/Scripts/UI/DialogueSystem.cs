using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private float _delay = 0.03f;
    [SerializeField] private TMP_Text _text;

    public void Show(string text)
    {
        _panel.SetActive(true);
        StopAllCoroutines();
        StopCoroutine(Insert(text));
    }

    public void Hide()
    {
        _panel.SetActive(false);
        StopAllCoroutines();
    }


    private IEnumerator Insert(string text)
    {
        _text.text = "";
        var stringBuilder = new StringBuilder();
        for (int i = 0; i < text.Length; i++)
        {
            stringBuilder.Append(text[i]);
            if (_text.isTextOverflowing)
                stringBuilder.Remove(0, stringBuilder.Length-2);

            _text.text = stringBuilder.ToString();
            yield return new WaitForSeconds(_delay);
        }
    }
}
