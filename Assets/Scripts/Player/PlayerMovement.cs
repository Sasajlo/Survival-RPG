using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float _movementSpeed;

    [SerializeField]
    private float _acceleration;

    [SerializeField]
    private float _friction;

    private Vector2 _inputDirection;
    private Vector3 _velocity;

    public Vector3 direction => _velocity.normalized;

    private Animator _animator;

    private Camera _mainCamera;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        UpdateVelocity();
        UpdatePosition();
    }

    private void HandleInput()
    {
        _inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _inputDirection.Normalize();
    }

    private void UpdateVelocity()
    {
        // Get camera yaw (Y rotation only)
        float yaw = _mainCamera.transform.eulerAngles.y;

        // Rotate input direction by camera yaw
        Quaternion rotation = Quaternion.Euler(0, yaw, 0);
        Vector3 movementDirection = rotation * new Vector3(_inputDirection.x, 0.0f, _inputDirection.y);

        if (movementDirection.magnitude > 0)
        {
            _velocity += movementDirection * _acceleration * Time.deltaTime;
            if (_velocity.magnitude > _movementSpeed)
            {
                _velocity = _velocity.normalized * _movementSpeed;
            }
        }
        else
        {
            Vector3 velocity = _velocity.magnitude > 1 ? _velocity.normalized : _velocity;
            _velocity -= velocity * _friction * Time.deltaTime;
        }

        _animator.SetBool("IsMoving", _velocity.sqrMagnitude > 0.8f);
    }

    private void UpdatePosition()
    {
        transform.position += _velocity * Time.deltaTime;
    }
}
