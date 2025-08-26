using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public RaycastHit2D CastRay(Vector2 current, Vector2 target, float maxDistance)
    {
        return Physics2D.Raycast(current, target, maxDistance);
    }
}
