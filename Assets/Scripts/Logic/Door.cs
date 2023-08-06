using UnityEngine;


public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider _doorCollider;

    public void Open() => animator.SetBool("open", true);

    public void Close() => animator.SetBool("open", false);

    public void OnDoorOpened() // called in animation
        => _doorCollider.isTrigger = true;

    public void OnDoorClosed() // called in animation
        => _doorCollider.isTrigger = false;
}
    
