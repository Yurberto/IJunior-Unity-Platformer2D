using UnityEngine;

public static class Utils 
{
    public const float PercentValue = 100f;
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
    }

    public static void DrawLine2D(Vector2 start, Vector2 end, Color color, float duration = 0.1f)
    {
        Vector3 start3D = new Vector3(start.x, start.y, 0);
        Vector3 end3D = new Vector3(end.x, end.y, 0);
        Debug.DrawLine(start3D, end3D, color, duration);
    }

    public static void DrawRay2D(Vector2 start, Vector2 end, Color color, float duration = 0.1f)
    {
        Vector3 start3D = new Vector3(start.x, start.y, 0);
        Vector3 direction3D = new Vector3(end.x, end.y, 0);
        Debug.DrawRay(start3D, direction3D, color, duration);
    }
}
