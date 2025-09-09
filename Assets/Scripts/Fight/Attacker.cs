using UnityEngine;

[RequireComponent(typeof(DamageableDetector))]
public class Attacker : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _damage = 10;
    [SerializeField, Range(0, 10)] private float _attackRange = 3f;

    private DamageableDetector _detector;

    private void Awake()
    {
        _detector = GetComponent<DamageableDetector>();
    }

    public void TryAttack()
    {
        if (_detector.TryDetect(out IDamageable damageable, _attackRange))
        {
            damageable.TakeDamage(_damage);
            Debug.Log("damagnulo");
        }
        else Debug.Log("νθυσÿ νε damagnulo");
    }
}
