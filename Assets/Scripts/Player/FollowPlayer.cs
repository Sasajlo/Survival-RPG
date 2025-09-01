using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform _playerTransform;

    [SerializeField]
    private Vector3 _followOffset;

    [SerializeField]
    private float _followSpeed;

    private void Awake()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = _playerTransform.position + _followOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);
    }
}
