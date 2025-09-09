using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TargetDetector : MonoBehaviour
{
    public event Action<Player> PlayerDetected;
    public event Action PlayerLost;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
            PlayerDetected?.Invoke(player);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
            PlayerLost?.Invoke();
    }
}
