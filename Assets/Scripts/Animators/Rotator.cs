using UnityEngine;

public class Rotator : MonoBehaviour
{
    private const float RightDirection = 1;
    private const float RightRotationInDeg = 0;
    private const float LeftRotationInDeg = 180;

    private float _currentDirection;

    public float LookDirection => _currentDirection;

    public void LookAt(float inputDirection)
    {
        _currentDirection = inputDirection.Normilize();

        bool isInputDirectionRight = Mathf.Approximately(_currentDirection, RightDirection);
        float updatedRotationY = isInputDirectionRight ? RightRotationInDeg : LeftRotationInDeg;

        Vector3 updatedRotation = new Vector3(transform.rotation.eulerAngles.x, updatedRotationY, transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Euler(updatedRotation);
    }
}