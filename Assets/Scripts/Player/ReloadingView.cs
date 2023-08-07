using System.Collections;
using UnityEngine;

public class ReloadingView : MonoBehaviour
{
    public IEnumerator Show()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
        yield break;
    }
} 
