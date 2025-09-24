public class Health : Resources, IDamageable
{
    public void Heal(float healAmount)
    {
        Increase(healAmount);
    }

    public void TakeDamage(float damage)
    {
        Decrease(damage);
    }
}
