using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _mainCamera;

    void Awake()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localRotation = _mainCamera.transform.rotation;
    }
}
