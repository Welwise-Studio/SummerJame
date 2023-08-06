using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SocialLink : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string _link;
    public void OnPointerDown(PointerEventData eventData)
    {
        Application.OpenURL(_link);
    }
}
