using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action<float> MovementKeyPressed;
    public event Action MovementKeyReleased;
    public event Action JumpKeyPressed;
    public event Action AttackKeyPressed;

    private void Update()
    {
        float direction = Input.GetAxisRaw(AxisData.Horizontal);

        if (direction != 0)
        {
            MovementKeyPressed?.Invoke(direction);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            MovementKeyReleased?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            JumpKeyPressed?.Invoke();

        if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Q))
            AttackKeyPressed?.Invoke();
    }

}
