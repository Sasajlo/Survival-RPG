using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    
    private DirectionManager _directionManager;

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _directionManager = GetComponent<DirectionManager>();
    }


    void Update()
    {
        // Update  direction based on movement direction
        _directionManager.SetDirection(_playerMovement.direction);
    }
}
