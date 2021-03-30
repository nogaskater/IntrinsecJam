using UnityEngine;

public class MyUtilities
{
    public static Vector2 DirectionVector(Vector3 from, Vector3 to)
    {
        return new Vector2(to.x, 0) - new Vector2(from.x, 0);
    }

}
