using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject _hand;

    GameManager _gameManagerScript;
    Rigidbody2D _enemyRb;

    float _speed = 5.0f;

    bool _isAttacked = false;
    bool _isCollideWithBase = false;

    // Start is called before the first frame update
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        _gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManagerScript.IsPlaying)
        {
            MoveForward();
            Attack();
        }

        else
        {
            DestroyWhenGameOver();
        }
        
    }

    void MoveForward()
    {
        _enemyRb.MovePosition(transform.position + Vector3.right * _speed * Time.deltaTime);
    }

    void Attack()
    {
        if (_isCollideWithBase && !_isAttacked)
        {
            _hand.SetActive(true);
            _isAttacked = true;
            StartCoroutine(TurnOffHand());
            StartCoroutine(AttackRate());
        }
    }

    IEnumerator AttackRate()
    {
        // Speed Rate
        yield return new WaitForSeconds(1.5f);
        _isAttacked = false;
    }

    IEnumerator TurnOffHand()
    {
        // Speed animation
        yield return new WaitForSeconds(0.5f);
        _hand.SetActive(false);
    }

    void DestroyWhenGameOver()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            _isCollideWithBase = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            _isCollideWithBase = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
