using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;

    // Update is called once per frame
    void Update()
    {
        if (_target)
        {

        }
    }

    public void SetTarget(Transform t) => _target = t;
}
