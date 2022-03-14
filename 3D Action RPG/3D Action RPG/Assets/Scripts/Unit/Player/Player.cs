using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{

    protected override void Awake()
    {
        base.Awake();
        CameraController.Instance.SetTarget(transform);
    }

    public void OnShowParry()
    {
        isParry = true;
    }

    public void ClearState()
    {
        isParry = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
    }
}
