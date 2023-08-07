using UnityEngine;

public class FinalDoorOpener : MonoBehaviour
{
    [SerializeField] private Door _door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBody player))
        {
            _door.Open();
        }
    }
}
