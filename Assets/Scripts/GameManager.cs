using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefabs;
    [SerializeField] TextMeshProUGUI _heathText;
    [SerializeField] TextMeshProUGUI _ammoText;
    [SerializeField] GameObject _gameOver;
    [SerializeField] GameObject _youWin;
    [SerializeField] GameObject _base;
    [SerializeField] GameObject _playAgainMenu;


    Base _baseScript;
    PlayerController _playerController;

    int _maximumEnemies = 20;
    int _enemiesSpawned = 0;
    int _posX = -17;

    float _rangeY = 3.7f;
    float _startTime = 2.0f;
    float _timeRate = 1.0f;

    bool _isPlaying = true;

    public bool IsPlaying 
    {
        get { return _isPlaying; }

        private set
        {
            _isPlaying = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _baseScript = _base.GetComponent<Base>();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("SpawnEnemy", _startTime, _timeRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlaying)
        {
            CheckWinCondition();
            GameOver();
            DisplayHeath();
            DisplayAmmo();
        }

        else
        {
            DisplayMenu();
        }
    }

    void GameOver()
    {
        if (_baseScript.Heath == 0)
        {
            IsPlaying = false;
            Destroy(_base);
            _gameOver.SetActive(true);
        }
    }

    void DisplayHeath()
    {
        _heathText.SetText("Heath: " + _baseScript.Heath);
    }

    void DisplayAmmo()
    {
        _ammoText.SetText("Ammo: " + _playerController.AmmoTotal);
    }

    void DisplayMenu()
    {
        _playAgainMenu.SetActive(true);
    }

    void SpawnEnemy()
    {
        if (_enemiesSpawned <= _maximumEnemies && _isPlaying)
        {
            Instantiate(_enemyPrefabs, RandomEnemyPos(), _enemyPrefabs.transform.rotation);
            _enemiesSpawned++;
        }
    }

    Vector2 RandomEnemyPos()
    {
        Vector2 result;
        float randomPosY = Random.Range(-_rangeY, _rangeY);
        result = new Vector2(_posX, randomPosY);
        return result;
    }

    void CheckWinCondition()
    {
        if (_enemiesSpawned >= _maximumEnemies && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            _isPlaying = false;
            _youWin.SetActive(true);
        }
    }
}
