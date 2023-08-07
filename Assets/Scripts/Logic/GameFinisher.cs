using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] private GameEnd _endTitles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBody player))
        {
            player.Player.DisableControls();
            _endTitles.Init();
        }

    }
}
