using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    private Transform target;
    [SerializeField] private Transform startPosition;

    private float offsetRange = 10f;
    private bool _isOutOfSafeArea = false;
    public bool isOutOfSafeArea => _isOutOfSafeArea;

    private float turnValue = 0.5f;
    private float moveValue = 0.25f;

    public Action DetectorRefresh;

    protected override void Awake()
    {
        base.Awake();
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
        //Debug.Log($"{name} : {(t != null ? t.name : "null")} ::: {_isOutOfSafeArea}");
        if (t == null)
        {
            target = null;
            SetInputVectorY(0f);
            SetInputVectorX(0f);
        }
        if (_isOutOfSafeArea)
            return;
        target = t;
    }

    private float walkVisionOffset = 2.5f;
    private float stopTurnOffset = 1.5f;
    private void MoveToTarget()
    {
        FaceToTarget(target);

        Vector3 localPos = transform.InverseTransformPoint(target.transform.position);
        if (localPos.x >= -walkVisionOffset && localPos.x <= walkVisionOffset)
            SetInputVectorY(moveValue);
        else
            SetInputVectorY(0f);

        // TODO if move out of spawn range back to original poistion
        if (IsOutOfAreaOffset())
        {
            _isOutOfSafeArea = true;
            SetInputVectorY(0f);
            SetTarget(null);
        }
    }

    private void FaceToTarget(Transform target)
    {
        var size = transform.GetTargetSide(target, stopTurnOffset);

        switch (size)
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
        if (_isOutOfSafeArea)
        {
            var distance = Vector3.Distance(transform.position, startPosition.position);

            //Debug.Log($"StayInSafeArea {distance}");
            if (distance < 1.5f)
            {
                SetInputVectorY(0f);
                _isOutOfSafeArea = false;
                DetectorRefresh?.Invoke();
            }
            else
            {
                FaceToTarget(startPosition);

                Vector3 localPos = transform.InverseTransformPoint(startPosition.position);
                if (localPos.x >= -walkVisionOffset && localPos.x <= walkVisionOffset)
                    SetInputVectorY(moveValue);
                else
                    SetInputVectorY(0f);
            }
        }
        else
        {
            // move around place
        }
    }

    private bool IsOutOfAreaOffset() => Vector3.Distance(transform.position, startPosition.position) > offsetRange;
}
