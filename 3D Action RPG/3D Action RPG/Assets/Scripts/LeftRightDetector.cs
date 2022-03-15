using UnityEngine;

public static class LeftRightDetector
{
    public static LRC GetTargetSide(this Transform _transform, Transform _target, float offset = 0f, bool isBehindCheck = true)
    {
        var returnValue = LRC.Center;
        Vector3 localPos = _transform.InverseTransformPoint(_target.position);
        var angleSum = Mathf.Abs(_transform.rotation.eulerAngles.y - _target.rotation.eulerAngles.y);
        var isOutOfOffset = (angleSum <= 30f) || (angleSum >= 320f);

        if (localPos.x < -offset)
            returnValue = LRC.Left;
        else if (localPos.x > offset)
            returnValue = LRC.Right;
        if(returnValue.Equals(LRC.Center) && isBehindCheck)
            if(isOutOfOffset)
                returnValue = LRC.Left;

        return returnValue;
    }
}

public enum LRC { Center = 0, Left = 1, Right = 2 }