using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int IsRun = Animator.StringToHash(nameof(IsRun));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
    }
}
