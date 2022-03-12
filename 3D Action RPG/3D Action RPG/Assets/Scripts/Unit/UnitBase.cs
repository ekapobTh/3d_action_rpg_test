using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour,IUnit
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

    private float turnSpeed = 250f;
    private float moveSpeed = 10f;

    protected virtual void Awake()
    {
        unitHP = UnitBaseValue.UNIT_MAX_HP;
        unitATK = UnitBaseValue.UNIT_ATTACK_DAMAGE;
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public void Hurt()
    {
        throw new System.NotImplementedException();
    }

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
