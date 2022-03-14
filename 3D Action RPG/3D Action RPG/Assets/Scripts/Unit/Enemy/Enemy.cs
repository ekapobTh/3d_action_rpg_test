using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    private Transform target;
    private Vector3 startPosition;

    private float offsetRange = 10f;
    private bool _isOutOfSafeArea = false;

    public float turnValue = 0.5f;

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

    public void SetTarget(Transform t) => target = t;

    private void MoveToTarget()
    {
        Vector3 localPos = transform.InverseTransformPoint(target.transform.position);

        //transform.rotation = Quaternion.Lerp(transform.rotation, rotationAngle, Time.deltaTime * 2f); 

        if (localPos.x < -0.15f) // left
           SetInputVectorX(-turnValue);
        else if (localPos.x > 0.15f) // right
            SetInputVectorX(turnValue);
        else // center
        {
            SetInputVectorX(0);
        }
        //Debug.Log($"MoveToTarget {angle} {angleAxis}");
        // move to target if move out of spawn range back to original poistion
        if (IsOutOfAreaOffset())
            SetTarget(null);
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
