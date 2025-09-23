using UnityEngine;

public class Rotator : MonoBehaviour
{
    private const float RightDirection = 1;
    private const float LeftDirection = -1;

    private float _currentDirection;

    public float LookDirection => _currentDirection;

    public void LookAt(float direction)
    {
        float rotationY = transform.rotation.eulerAngles.y;

        _currentDirection = (rotationY % (2 * Utils.PiInDeg) == 0) ? RightDirection : LeftDirection;

        float currentRotationY = (direction.Normilize() == RightDirection) ? 0 : Utils.PiInDeg;
        Vector3 currentRotation = new Vector3(transform.rotation.eulerAngles.x, currentRotationY, transform.rotation.eulerAngles.z);

        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
