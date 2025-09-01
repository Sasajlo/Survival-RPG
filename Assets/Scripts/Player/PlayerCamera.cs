using Unity.AppUI.Core;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Camera _mainCamera;

    private float _cameraAngle = 0.0f;
    private float _targetAngle = 0.0f;

    [SerializeField]
    private Vector3 _followOffset;

    private Vector3 _zoomedOffset;
    private Vector3 _rotatedOffset;

    [SerializeField]
    private float _rotationSpeed = 50.0f;

    [SerializeField]
    private float _zoomSpeed;

    [SerializeField]
    private Vector2 _zoomIncrement;

    [SerializeField]
    private float _minZoom;

    [SerializeField]
    private float _maxZoom;


    void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Start()
    {
        _zoomedOffset = _followOffset;
    }

    void Update()
    {
        UpdateZoom();
        UpdateOffset();
        UpdateCamera();
    }

    void UpdateZoom()
    {
        Vector3 deltaScroll = new Vector3(0, _zoomIncrement.y, _zoomIncrement.x) * Input.mouseScrollDelta.y * Time.deltaTime;

        if (_followOffset.y - deltaScroll.y >= _minZoom && _followOffset.y - deltaScroll.y <= _maxZoom)
            _followOffset -= deltaScroll;

        _zoomedOffset = Vector3.Lerp(_zoomedOffset, _followOffset, _zoomSpeed * Time.deltaTime);
    }

    private void UpdateOffset()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _targetAngle -= 45.0f;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _targetAngle += 45.0f;
        }

        _cameraAngle = Mathf.Lerp(_cameraAngle, _targetAngle, _rotationSpeed * Time.deltaTime);

        // Convert angle to radians
        float angleRad = _cameraAngle * Mathf.Deg2Rad;

        // Rotation matrix
        float cos = Mathf.Cos(angleRad);
        float sin = Mathf.Sin(angleRad);

        // Apply rotation
        _rotatedOffset = new Vector3(
            _zoomedOffset.x * cos - _zoomedOffset.z * sin,
            _zoomedOffset.y,
            _zoomedOffset.x * sin + _zoomedOffset.z * cos
        );
    }

    private void UpdateCamera()
    {
        _mainCamera.transform.position = transform.position + _rotatedOffset;
        _mainCamera.transform.rotation = Quaternion.LookRotation(transform.position - _mainCamera.transform.position);
    }
}
