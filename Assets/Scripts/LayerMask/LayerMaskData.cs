using UnityEngine;

public static class LayerMaskData
{
    public static readonly LayerMask Player = LayerMask.GetMask(nameof(Player));
    public static readonly LayerMask Enemy = LayerMask.GetMask(nameof(Enemy));
    public static readonly LayerMask Ground = LayerMask.GetMask(nameof(Ground));
    public static readonly LayerMask Item = LayerMask.GetMask(nameof(Item));
    public static readonly LayerMask Decoration = LayerMask.GetMask(nameof(Decoration));
    public static readonly LayerMask Trigger = LayerMask.GetMask(nameof(Trigger));
    public static readonly LayerMask NonPhysical = LayerMask.GetMask(nameof(NonPhysical));

    public static class InString
    {
        public static readonly string Player = nameof(Player);
        public static readonly string Enemy = nameof(Enemy);
        public static readonly string Ground = nameof(Ground);
        public static readonly string Item = nameof(Item);
        public static readonly string Decoration = nameof(Decoration);
        public static readonly string Trigger = nameof(Trigger);
        public static readonly string NonPhysical = nameof(NonPhysical);
    }
}
