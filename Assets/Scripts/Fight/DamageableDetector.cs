using UnityEngine;
using Color = UnityEngine.Color;

[RequireComponent(typeof(Collider2D))]
public class DamageableDetector : MonoBehaviour
{
    private Collider2D _characterCollider;

    private void Awake()
    {
        _characterCollider = GetComponent<Collider2D>();
    }

    public bool TryDetect(out IDamageable detected, float attackRange)
    {
        Vector2[] castPoints = new Vector2[]
        {
            new Vector2(_characterCollider.bounds.center.x, _characterCollider.bounds.max.y),
            new Vector2(_characterCollider.bounds.center.x, _characterCollider.bounds.center.y),
            new Vector2(_characterCollider.bounds.center.x, _characterCollider.bounds.min.y)
        };

        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);
        Debug.Log(Mathf.Sign(transform.localScale.x));

        foreach (var point in castPoints)
        {
            RaycastHit2D hit = Physics2D.Raycast(point, direction, attackRange);
            Debug.DrawRay(point, direction * attackRange, Color.red, 1);
            Debug.Log(hit.collider);


            if (hit.collider != null && hit.collider != _characterCollider && hit.collider.TryGetComponent(out Enemy health))
            {
                //detected = health;
                Debug.Log("Detected");
                //return true;
            }
        }
        Debug.Log("νθυσÿ νε Detected");

        detected = null;
        return false;
    }
}
