using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private float _moveSpeed = 2;

    public void Move(Vector2 offset)
    {
        transform.Translate(offset * (_moveSpeed * Time.deltaTime));
    }
}
