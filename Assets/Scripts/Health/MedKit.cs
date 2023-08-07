using UnityEngine;

public class MedKit : MonoBehaviour
{
    [SerializeField] private int _healAmount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.AddHealth(_healAmount);
            Destroy(gameObject);
        }

    }
}
