using UnityEngine;

public static class Utils 
{
    public const float PiInDeg = 180f;

    public static void DrawBox(Vector2 boxCenter, Vector2 boxSize, float duration = 1)
    {
        Vector2 halfSize = boxSize * 0.5f;
        Vector2 topLeft = new Vector2(boxCenter.x - halfSize.x, boxCenter.y + halfSize.y);
        Vector2 topRight = new Vector2(boxCenter.x + halfSize.x, boxCenter.y + halfSize.y);
        Vector2 bottomLeft = new Vector2(boxCenter.x - halfSize.x, boxCenter.y - halfSize.y);
        Vector2 bottomRight = new Vector2(boxCenter.x + halfSize.x, boxCenter.y - halfSize.y);

        Debug.DrawLine(topLeft, topRight, Color.red, duration);
        Debug.DrawLine(topRight, bottomRight, Color.red, duration);
        Debug.DrawLine(bottomRight, bottomLeft, Color.red, duration);
        Debug.DrawLine(bottomLeft, topLeft, Color.red, duration);

        Debug.Log("Draw");
    }
}
