using System;
using UnityEditor.Animations;
using UnityEngine;

public class DirectionManager : MonoBehaviour
{
    private Vector3 _direction;

    private Animator _animatorController;
    
    private Camera _mainCamera;

    public void SetDirection(Vector3 direction)
    {
        if (direction.magnitude > 0)
        {
            _direction = direction;
            _direction.Normalize();
        }
    }

    void Awake()
    {
        _animatorController = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    void Update()
    {        
        // Flatten camera forward on ground plane (XZ)
        Vector3 camForward = _mainCamera.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        // Flatten camera right on ground plane (XZ)
        Vector3 camRight = _mainCamera.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        // Convert world direction into "camera space"
        float x = Vector3.Dot(_direction, camRight);
        float y = Vector3.Dot(_direction, camForward);

        Vector2 screenDirection = new Vector2(x, y).normalized;

        // Animator params
        _animatorController.SetFloat("DirectionX", screenDirection.x);
        _animatorController.SetFloat("DirectionY", screenDirection.y);
    }
}
