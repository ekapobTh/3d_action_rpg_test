using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour, IUnit
{
    #region Unit Data
    protected int unitHP;
    protected int unitATK;
    #endregion

    private float moveInput = 0f;
    private float turnInput = 0f;

    #region Component
    [SerializeField] private Animator m_Animator;
    #endregion

    #region Animation Parameter
    private const string ATTACK_TRIGGER = "Attack";
    private const string PARRY_TRIGGER = "Parry";
    private const string DAMAGE_TRIGGER = "Damage";

    private const string ATTACK = "isAttack";
    private const string PARRY = "isParry";
    #endregion

    protected bool isDamaged;
    protected bool isParry;
    protected bool isAttack;
    protected bool isDeath;

    private float turnSpeed = 250f;
    private float moveSpeed = 10f;

    protected virtual void Awake()
    {
        isDamaged = false;
        isParry = false;
        isAttack = false;
        isDeath = false;

        unitHP = UnitBaseValue.UNIT_MAX_HP;
        unitATK = UnitBaseValue.UNIT_ATTACK_DAMAGE;
    }

    public void Attack()
    {
        if (isUnableToAction(isParry || isDeath))
            return;
        throw new NotImplementedException();
    }

    public void Parry()
    {
        if (isUnableToAction(isAttack || isDeath))
            return;
        throw new NotImplementedException();
    }

    public void Death()
    {
        throw new NotImplementedException();
    }

    public void Hurt()
    {
        if (isUnableToAction(isDeath))
            return;
        throw new NotImplementedException();
    }

    protected virtual bool isUnableToAction(bool elseCondition = false) => isDamaged || elseCondition;

    public void SetInputVector(Vector2 inputV)
    {
        SetInputVectorX(inputV.x);
        SetInputVectorY(inputV.y);
    }
    public void SetInputVectorX(float inputX) => turnInput = inputX;
    public void SetInputVectorY(float inputY) => moveInput = inputY;


    private void Rotate()
    {
        transform.Rotate(Vector3.up, turnInput * turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();

        Move();
    }
}