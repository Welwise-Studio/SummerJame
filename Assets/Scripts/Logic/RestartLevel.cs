using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    private void Awake()
    {
        MapGlobals.Instance.Player.OnDie += () => StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
        yield break;
    }
}
