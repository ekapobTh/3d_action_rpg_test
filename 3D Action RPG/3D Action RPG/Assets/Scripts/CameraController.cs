using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController _instance;
    public static CameraController Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<CameraController>();
            return _instance;
        }
    }

    [SerializeField] private Transform _target;

    [SerializeField] private float _mouseSensitivity = 3f;
    public bool isReverse;

    private float _rotationY;
    private float _rotationX;

    [SerializeField] private float _distanceFromTarget = 3f;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField] private float _smoothTime = 0.2f;

    [SerializeField] private Vector2 _rotationMinMax = new Vector2(-40f, 40f);

    // Update is called once per frame
    void Update()
    {
        if (_target)
            GenerateCameraMovement();
    }

    public void SetTarget(Transform t) => _target = t;

    public void GenerateCameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationY += mouseX;
        _rotationX += mouseY;

        _rotationX = Mathf.Clamp(_rotationX, _rotationMinMax.x, _rotationMinMax.y);

        Vector3 nextRotation = new Vector3(_rotationX * (isReverse ? -1 : 1), _rotationY * (isReverse ? -1 : 1));

        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.eulerAngles = _currentRotation;

        transform.position = _target.position - transform.forward * _distanceFromTarget;
    }
}// https://youtu.be/owW7BE2t8ME?t=114
