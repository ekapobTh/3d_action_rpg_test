using System;
using UnityEngine;

public class Enemy : UnitBehavior
{
    private UnitBehavior target;
    [SerializeField] private Transform startPosition;
    [SerializeField] private EnemyHPBar m_EnemyHPBar;

    private float offsetRange = 10f;
    private bool _isOutOfSafeArea = false;
    public bool isOutOfSafeArea => _isOutOfSafeArea;

    private float turnValue = 0.5f;
    private float moveValue = 0.25f;

    public Action DetectorRefresh;

    public void SetStartTransform(Transform t) => startPosition = t;

    protected override void Awake()
    {
        base.Awake();
        m_EnemyHPBar.ForceUpdateHP(unitHP);
        hurtAction = EnemyUpdateHP;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (IsDeath())
            return;
        base.Update();
        // Set X and Y to move
        if (target != null)
            MoveToTarget();
        else
            StayInSafeArea();
    }

    void EnemyUpdateHP() => m_EnemyHPBar.UpdateHP(unitHP);

    public void SetTarget(Transform t)
    {
        //Debug.Log($"{name} : {(t != null ? t.name : "null")} ::: {_isOutOfSafeArea}");
        if (t == null)
        {
            target = null;
            SetInputVectorY(0f);
            SetInputVectorX(0f);
            return;
        }
        if (_isOutOfSafeArea)
            return;
        target = t.GetComponent<UnitBehavior>();
    }

    private float walkVisionOffset = 2.5f;
    private float stopTurnOffset = 1.5f;
    private void MoveToTarget()
    {
        if (IsOutOfAreaOffset())
            SetupForBack();
        else
        {
            var distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= attackVision)
            {
                SetInputVectorY(0f);

                var currentFaceing = FaceToTarget(target.transform, 0.05f, false);

                if (currentFaceing == LRC.Center)
                {
                    if (!target.IsDeath())
                        Attack();
                    else
                        SetupForBack();
                }
            }
            else
            {
                FaceToTarget(target.transform, stopTurnOffset);

                Vector3 localPos = transform.InverseTransformPoint(target.transform.position);

                if (localPos.x >= -walkVisionOffset && localPos.x <= walkVisionOffset)
                    SetInputVectorY(moveValue);
                else
                    SetInputVectorY(0f);
            }
        }


        void SetupForBack()
        {
            _isOutOfSafeArea = true;
            SetInputVectorY(0f);
            SetTarget(null);
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