using UnityEngine;

public class ItemCollector : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Gem gem))
            gem.PikeUp();
    }
}
