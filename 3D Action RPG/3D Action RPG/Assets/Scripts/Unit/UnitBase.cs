using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour,IUnit
{
    #region Unit Data
    protected int unitHP;
    #endregion

    #region Component
    [SerializeField] private Animator m_Animator;
    #endregion

    protected virtual void Awake()
    {
        unitHP = UnitConstantData.UNIT_MAX_HP;
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
}
