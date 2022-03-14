using UnityEngine;

public static class LeftRightDetector
{
    public static LRC GetTargetSide(this Transform _transform, Transform _target, float offset = 0f)
    {
        Vector3 localPos = _transform.InverseTransformPoint(_target.position);

        if (localPos.x < -offset)
            return LRC.Left;
        else if (localPos.x > offset)
            return LRC.Right;
        else
            return LRC.Center;
    }
}

public enum LRC { Center = 0, Left = 1, Right = 2 }