using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationEventHandler : MonoBehaviour
{
    private UnitBehavior m_UnitBehavior;

    private void Awake()
    {
        m_UnitBehavior = transform.parent.GetComponent<UnitBehavior>();
    }

    public void OnShowParry()
    {
        m_UnitBehavior.OnShowParry();
    }

    public void OnStartAnimation()
    {
        m_UnitBehavior.ClearState();
    }

    public void OnAttacking() => m_UnitBehavior.Attacked();
}
