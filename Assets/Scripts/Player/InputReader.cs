using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action<float> MovementKeyPressed;
    public event Action MovementKeyReleased;
    public event Action JumpKeyPressed;


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
    }

}
