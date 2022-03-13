﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    [SerializeField] private UnitSpawner m_UnitSpawner;

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        var cameraScript = Camera.main.GetComponent<CameraController>();

        // cameraScript.SetTarget(); setup player & show start button
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}