using System;
using UnityEngine;

public class UnitBehavior : MonoBehaviour, IUnit
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
    protected const string DEATH_TRIGGER = "Death";

    protected const string ATTACK = "isAttack";
    protected const string PARRY = "isParry";

    protected const string MOVE = "Move";
    #endregion

    #region Constant
    protected const int UNIT_MAX_HP = 100;
    protected const int UNIT_ATTACK_DAMAGE = 10;
    protected const float UNIT_ATTACK_COOLDOWN = 1f;
    #endregion

    #region Action
    protected Action hurtAction;
    protected Action deathAction;
    #endregion

    protected bool _isDamaged;
    protected bool _isParry;
    protected bool _isAttack;
    protected bool _isDeath;

    private float turnSpeed = 250f;
    private float moveSpeed = 10f;
    private float parryAngleDot = 0.75f;

    protected float attackVision = 1.5f;
    protected bool isContinue = false;
    protected UnitBehavior targetBehavior;

    public void SetAttackTarget(UnitBehavior unitTarget) => targetBehavior = unitTarget;
    public bool IsDeath() => _isDeath;

    private float attackCooldown = 0f;

    protected virtual void Awake()
    {
        _isDamaged = false;
        _isParry = false;
        _isAttack = false;
        _isDeath = false;

        unitHP = UNIT_MAX_HP;
        unitATK = UNIT_ATTACK_DAMAGE;
    }

    protected virtual void Update()
    {
        if (attackCooldown > 0f)
            attackCooldown -= Time.deltaTime;
    }

    #region Animation
    public void Attack()
    {
        if (isUnableToAction(_isParry) || attackCooldown > 0f)
            return;
        attackCooldown = UNIT_ATTACK_COOLDOWN;
        m_Animator.SetTrigger(ATTACK_TRIGGER);
        m_Animator.SetBool(ATTACK, isContinue);
    }

    public void UnAttack()
    {
        m_Animator.SetBool(ATTACK, false);
    }

    public void Parry()
    {
        if (isUnableToAction(_isAttack))
            return;
        m_Animator.SetTrigger(PARRY_TRIGGER);
        m_Animator.SetBool(PARRY, true);
    }
    public void OnShowParry() => _isParry = true;

    public void UnParry()
    {
        m_Animator.SetBool(PARRY, false);
    }

    public void Death()
    {
        m_Animator.SetTrigger(DEATH_TRIGGER);
        _isDeath = true;
        DeathSet();
    }

    public void Hurt(int damage, UnitBehavior hitter)
    {
        if (isUnableToAction())
            return;
        if (_isParry)
        {
            var angleDot = Quaternion.Dot(transform.rotation, hitter.transform.rotation);

            if (angleDot > parryAngleDot)
                doHurt();
        }
        else
            doHurt();
        if (unitHP <= 0)
            Death();

        void doHurt()
        {
            unitHP -= damage;
            hurtAction?.Invoke();
        }
    }

    public void ClearState()
    {
        _isParry = false;
    }

    public void MoveUpdate()
    {
        m_Animator.SetFloat(MOVE, moveInput);
    }

    public void Attacked()
    {
        if (targetBehavior == null)
            return;
        targetBehavior.Hurt(unitATK, this);
    }
    #endregion 

    protected virtual bool isUnableToAction(bool elseCondition = false) => _isDamaged || _isDeath || elseCondition;

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

    private void DeathSet()
    {
        tag = UnitTag.UNTAGGED;
        deathAction?.Invoke();
    }
}