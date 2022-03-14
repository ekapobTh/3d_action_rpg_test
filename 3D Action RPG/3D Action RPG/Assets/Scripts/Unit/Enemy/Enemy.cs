using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    private Transform target;
    private Vector3 startPosition;

    private float offsetRange = 10f;
    private bool _isOutOfSafeArea = false;

    private float turnValue = 0.5f;
    private float moveValue = 0.25f;

    protected override void Awake()
    {
        base.Awake();
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Set X and Y to move
        if (target != null)
            MoveToTarget();
        else
            StayInSafeArea();
    }

    public void SetTarget(Transform t)
    {
        Debug.Log($"{name} : {(t != null ? t.name : "null")}");
        target = t;
    }

    private float walkVisionOffset = 2.5f;
    private float stopTurnOffset = 1.5f;
    private void MoveToTarget()
    {
        FaceToTarget();

        Vector3 localPos = transform.InverseTransformPoint(target.transform.position);
        if (localPos.x >= -walkVisionOffset && localPos.x <= walkVisionOffset)
            SetInputVectorY(moveValue);
        else
            SetInputVectorY(0f);

        // TODO if move out of spawn range back to original poistion
        if (IsOutOfAreaOffset())
        {
            SetInputVectorY(0f);
            SetTarget(null);
        }
    }

    private void FaceToTarget()
    {
        switch (transform.GetTargetSide(target.transform, stopTurnOffset))
        {
            case LRC.Left:
                {
                    SetInputVectorX(-turnValue);
                }
                break;
            case LRC.Right:
                {
                    SetInputVectorX(turnValue);
                }
                break;
            default:
                {
                    SetInputVectorX(0);
                }
                break;
        }
    }
    
    private void StayInSafeArea()
    {
        if (!_isOutOfSafeArea)
            return;
        // move to startPosition
        // _isOutOfSafeArea = true; // after back to default path
    }

    private bool IsOutOfAreaOffset()
    {
        var sumPosition = transform.localPosition - startPosition;
        var sumPositionResult = Mathf.Abs(sumPosition.x) + Mathf.Abs(sumPosition.z);

        return sumPositionResult > offsetRange;
    }
}
