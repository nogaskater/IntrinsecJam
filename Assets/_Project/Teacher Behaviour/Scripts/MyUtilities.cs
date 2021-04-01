using UnityEngine;

public class MyUtilities
{
    public static Vector2 DirectionVectorX(Vector3 from, Vector3 to)
    {
        return new Vector2(to.x, 0) - new Vector2(from.x, 0);
    }

    public static Vector3 DirectionVectorZ(Vector3 from, Vector3 to)
    {
        return new Vector3(0, 0, to.z) - new Vector3(0, 0, from.z);
    }

}
