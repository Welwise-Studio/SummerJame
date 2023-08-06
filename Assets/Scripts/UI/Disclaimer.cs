using System.Collections;
using UnityEngine;

public class Disclaimer : MonoBehaviour
{
    [SerializeField] private float _showDuration = 3.5f;
    [SerializeField] private float _fadeDuration = 0.3f;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Start()
    {

        _canvasGroup.alpha = 1f;
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(_showDuration);
        for (float t = 0; t < _fadeDuration; t += Time.deltaTime)
        {
            _canvasGroup.alpha = Mathf.Lerp(1,0,t/_fadeDuration);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        gameObject.SetActive(false);
    }
}
