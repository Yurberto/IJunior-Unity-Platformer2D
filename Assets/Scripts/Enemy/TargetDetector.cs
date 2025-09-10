using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TargetDetector : MonoBehaviour
{
    public event Action<Player> PlayerDetected;
    public event Action PlayerLost;

    public event Action DamageableDetected;
    public event Action DamageableLost;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            PlayerDetected?.Invoke(player);

            if (player.TryGetComponent(out IDamageable _))
                DamageableDetected?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            PlayerLost?.Invoke();

            if (player.TryGetComponent(out IDamageable _))
                DamageableLost?.Invoke();
        }
    }
}
