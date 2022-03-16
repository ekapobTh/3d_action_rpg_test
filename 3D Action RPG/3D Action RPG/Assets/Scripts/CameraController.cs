using UnityEngine;
using UnityEngine.UI;

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
    private bool isReverseX;
    private bool isReverseY;

    private float _rotationY;
    private float _rotationX;

    [SerializeField] private float _distanceFromTarget = 3f;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField] private float _smoothTime = 0.2f;

    [SerializeField] private Vector2 _rotationMinMax = new Vector2(-40f, 40f);

    [SerializeField] private Toggle hToggle;
    [SerializeField] private Toggle vToggle;

    private void Awake()
    {
        hToggle.onValueChanged.AddListener(ReverseHorizonMove);
        hToggle.onValueChanged.AddListener(ReverseVerticalMove);
    }

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

        Vector3 nextRotation = new Vector3(_rotationX * (isReverseX ? -1 : 1), _rotationY * (isReverseY ? 1 : -1));

        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.eulerAngles = _currentRotation;

        transform.position = _target.position - transform.forward * _distanceFromTarget;
    }

    public void ReverseHorizonMove(bool isReverse) => isReverseX = isReverse;
    public void ReverseVerticalMove(bool isReverse) => isReverseY = isReverse;
}