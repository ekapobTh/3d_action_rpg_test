﻿using System;
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
    protected const string ATTACK_TRIGGER = "Attack";
    protected const string PARRY_TRIGGER = "Parry";
    protected const string DAMAGE_TRIGGER = "Damage";
    protected const string DEATH_TRIGGER = "Damage";

    protected const string ATTACK = "isAttack";
    protected const string PARRY = "isParry";

    protected const string MOVE = "Move";
    #endregion

    #region Constant
    protected const int UNIT_MAX_HP = 100;
    protected const int UNIT_ATTACK_DAMAGE = 10;
    #endregion

    protected bool isDamaged;
    protected bool isParry;
    protected bool isAttack;
    protected bool isDeath;

    private float turnSpeed = 250f;
    private float moveSpeed = 10f;
    protected float attackVision = 1.5f;

    protected virtual void Awake()
    {
        isDamaged = false;
        isParry = false;
        isAttack = false;
        isDeath = false;

        unitHP = UNIT_MAX_HP;
        unitATK = UNIT_ATTACK_DAMAGE;
    }

    public void Attack()
    {
        if (isUnableToAction(isParry))
            return;
        m_Animator.SetTrigger(ATTACK_TRIGGER);
        m_Animator.SetBool(ATTACK, true);
        // TODO attack cooldown 1 sec
    }

    public void UnAttack()
    {
        m_Animator.SetBool(ATTACK, false);
    }

    public void Parry()
    {
        if (isUnableToAction(isAttack))
            return;
        m_Animator.SetTrigger(PARRY_TRIGGER);
        m_Animator.SetBool(PARRY, true);
    }

    public void UnParry()
    {
        m_Animator.SetBool(PARRY, false);
    }

    public void Death()
    {
        m_Animator.SetTrigger(DEATH_TRIGGER);
        isDeath = true;
    }

    public void Hurt()
    {
        if (isUnableToAction())
            return;
        throw new NotImplementedException();
    }

    public void MoveUpdate()
    {
        m_Animator.SetFloat(MOVE, moveInput);
    }

    protected virtual bool isUnableToAction(bool elseCondition = false) => isDamaged || isDeath || elseCondition;

    public void SetInputVector(Vector2 inputV)
    {
        SetInputVectorX(inputV.x);
        SetInputVectorY(inputV.y);
    }
    public void SetInputVectorX(float inputX) => turnInput = inputX;
    public void SetInputVectorY(float inputY)
    {
        moveInput = inputY;
        MoveUpdate();
    }


    private void Rotate()
    {
        transform.Rotate(Vector3.up, turnInput * turnSpeed * Time.deltaTime);
    }

    private void Movement()
    {
        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();

        Movement();
    }


}