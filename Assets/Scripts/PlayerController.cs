using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] Transform _gunPosition;
    [SerializeField] GameObject _gameManager;
    [SerializeField] GameObject _bullet;
    [SerializeField] Camera _camera;

    GameManager _gameManagerScript;

    Rigidbody2D _playerRb;

    float _horizontalInput;
    float _verticalInput;
    float _speed = 20.0f;
    float _ammoTotal = 50.0f;

    Vector3 _mousePos;

    bool _isShoot = false;
    bool _isReload = false;

    public float AmmoTotal
    {
        get
        {
            return _ammoTotal;
        }

        private set
        {
            if (value > 50)
            {
                value = 50;
            }

            else if (value < 0)
            {
                value = 0;
            }

            _ammoTotal = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _gameManagerScript = _gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManagerScript.IsPlaying)
        {
            PlayerInput();
            Aim();
        }
        
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            PlayerShooting();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _isShoot = true;
            _isReload = true;
            StartCoroutine(ReloadTime());
        }
    }

    void PlayerMovement()
    {
        if (_horizontalInput != 0)
        {
            _playerRb.MovePosition(transform.position + Vector3.right * _horizontalInput * _speed * Time.deltaTime);
        }

        if (_verticalInput != 0)
        {
            _playerRb.MovePosition(transform.position + Vector3.up * _verticalInput * _speed * Time.deltaTime);
        }
    }

    void Aim()
    {
        Vector3 lookDirection = _mousePos - _gunPosition.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 180;
        _gunPosition.rotation = Quaternion.Euler(0, 0, angle);
    }

    void PlayerShooting()
    {
        Reload();

        if (!_isShoot & !_isReload)
        {
            _isShoot = true;
            AmmoTotal--;
            Instantiate(_bullet, _gunPosition.position, _gunPosition.rotation);
            StartCoroutine(ShootingRate());
        }

    }

    IEnumerator ShootingRate()
    {
        yield return new WaitForSeconds(0.2f);
        _isShoot = false;

    }

    void Reload()
    {
        if (_ammoTotal <= 0)
        {
            _isShoot = true;
            _isReload = true;
            StartCoroutine(ReloadTime());
        }
    }

    IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(2);
        AmmoTotal = 50;
        _isReload = false;
        _isShoot = false;
    }

}
