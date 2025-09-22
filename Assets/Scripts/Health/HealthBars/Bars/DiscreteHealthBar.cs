public class DiscreteHealthBar : ImageHealthBar
{
    protected override void UpdateHealthData()
    {
        FillZone.ApplyFill(Health.CurrentValue / Health.MaxValue);
    }
}
