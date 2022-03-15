using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBehavior
{
    protected override void Awake()
    {
        base.Awake();
        CameraController.Instance.SetTarget(transform);
        isContinue = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
    }
}
