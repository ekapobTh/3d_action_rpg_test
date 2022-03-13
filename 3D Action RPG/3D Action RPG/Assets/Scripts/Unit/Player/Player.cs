using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{
    private bool _isParry;
    public bool isParry => _isParry;

    private void Awake()
    {
        CameraController.Instance.SetTarget(transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Parry()
    {
        // TODO
    }

    public void OnShowParry()
    {
        _isParry = true;
    }

    public void ClearState()
    {
        _isParry = false;
    }
}
