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
    protected override void Update()
    {
        base.Update();
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
        if (IsOutOfAreaOffset())
        {
            _isOutOfSafeArea = true;
            SetInputVectorY(0f);
            SetTarget(null);
        }
        else
        {
            var distance = Vector3.Distance(transform.position, target.position);

            if (distance <= attackVision)
            {
                SetInputVectorY(0f);

                var currentFaceing = FaceToTarget(target, 0.05f, false);

                if(currentFaceing == LRC.Center)
                    Attack();
            }
            else
            {
                FaceToTarget(target, stopTurnOffset);

                Vector3 localPos = transform.InverseTransformPoint(target.transform.position);

                if (localPos.x >= -walkVisionOffset && localPos.x <= walkVisionOffset)
                    SetInputVectorY(moveValue);
                else
                    SetInputVectorY(0f);
            }
        }

    }

    private LRC FaceToTarget(Transform target, float offset, bool isCheckBehind = true)
    {
        var size = transform.GetTargetSide(target, offset, isCheckBehind);

        switch (size)
        {
            case LRC.Left:
                {
                    SetInputVectorX(-turnValue);
                    return LRC.Left;
                }
            case LRC.Right:
                {
                    SetInputVectorX(turnValue);
                    return LRC.Right;
                }
            default:
                {
                    SetInputVectorX(0);
                    return LRC.Center;
                }
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
                FaceToTarget(startPosition, stopTurnOffset);

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
