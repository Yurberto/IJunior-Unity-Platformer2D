using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action<float> MovementKeyPressed;
    public event Action MovementKeyReleased;
    public event Action JumpKeyPressed;
    public event Action AttackKeyPressed;
    public event Action UltimateKeyPressed;

    private void Update()
    {
        float direction = Input.GetAxisRaw(AxisData.Horizontal);

        if (direction != 0)
            MovementKeyPressed?.Invoke(direction);
        else if (Input.GetKeyUp(InputData.MoveLeft) || Input.GetKeyUp(InputData.MoveRight))
            MovementKeyReleased?.Invoke();

        if (Input.GetKeyDown(InputData.Jump) || Input.GetKeyDown(InputData.AlternativeJump))
            JumpKeyPressed?.Invoke();

        if (Input.GetKeyDown(InputData.Attack) || Input.GetKeyDown(InputData.AlternativeAttack))
            AttackKeyPressed?.Invoke();

        if (Input.GetKeyDown(InputData.Ultimate))
            UltimateKeyPressed?.Invoke();
    }

}
