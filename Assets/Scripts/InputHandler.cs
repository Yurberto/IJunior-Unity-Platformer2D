using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<Vector2> MovementKeyPressed;

    private void Update()
    {
        Vector2 position = new Vector2(Input.GetAxisRaw(AxisConstants.Horizontal.ToString()), 0);

        if (position != Vector2.zero)
            MovementKeyPressed?.Invoke(position);
    }
}
