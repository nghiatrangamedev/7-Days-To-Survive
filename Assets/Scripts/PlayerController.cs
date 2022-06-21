using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _gunPosition;
    [SerializeField] GameObject _bullet;

    Rigidbody2D _playerRb;

    float _horizontalInput;
    float _verticalInput;
    float _mouseY;

    float _rotateSpeed = 500.0f;
    float _speed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        
        if (Input.GetMouseButtonDown(0))
        {
            PlayerShooting();
        }
    }

    void PlayerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        _mouseY = - Input.GetAxisRaw("Mouse Y");
    }

    void PlayerMovement()
    {
        if (_horizontalInput != 0)
        {
            _playerRb.MovePosition(transform.position + Vector3.right * _horizontalInput * _speed * Time.deltaTime);
        }

        if (_verticalInput != 0)
        {
            _playerRb.MovePosition(transform.position + Vector3.up *_verticalInput * _speed * Time.deltaTime);
        }
    }

    void PlayerShooting()
    {
        Instantiate(_bullet, _gunPosition.position, _bullet.transform.rotation);
    }

}
