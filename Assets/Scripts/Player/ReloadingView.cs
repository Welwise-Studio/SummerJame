using System.Collections;
using UnityEngine;

public class ReloadingView : MonoBehaviour
{
    public IEnumerator Show(float seconds)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
        yield break;
    }
} 
