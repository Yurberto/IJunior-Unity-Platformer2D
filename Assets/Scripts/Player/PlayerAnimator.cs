using UnityEngine;

[RequireComponent(typeof(Rotator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private Rotator _flipper;

    private void Awake()
    {
        _flipper = GetComponent<Rotator>();
    }

    private void OnEnable()
    {
        _inputReader.MovementKeyPressed += _flipper.LookAt;
    }

    private void OnDisable()
    {
        _inputReader.MovementKeyPressed -= _flipper.LookAt;
    }
}
