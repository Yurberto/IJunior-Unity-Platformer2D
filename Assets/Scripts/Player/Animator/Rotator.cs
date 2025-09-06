using UnityEngine;

public class Rotator : MonoBehaviour
{
    public void LookAt(float direction)
    {
        if (Mathf.Sign(direction) == Mathf.Sign(transform.localScale.x))
            return;

        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }
}
