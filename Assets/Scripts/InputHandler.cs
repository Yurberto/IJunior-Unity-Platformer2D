using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<Vector2> MovementKeyPressed;
    public event Action JumpKeyPressed;
    public event Action MovementStopped;

    private void Update()
    {
        Vector2 position = new Vector2(Input.GetAxisRaw(AxisConstants.Horizontal.ToString()), 0);

        if (position == Vector2.zero)
            MovementStopped?.Invoke();
        else
            MovementKeyPressed?.Invoke(position);

        if (Input.GetKeyDown(KeyCode.Space))
            JumpKeyPressed?.Invoke();
    }
}
