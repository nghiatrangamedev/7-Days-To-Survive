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

    Base _baseScript;
    PlayerController _playerController;

    int _maximumEnemies = 20;
    int _enemiesSpawned = 0;
    int _posX = -17;

    float _rangeY = 3.7f;
    float _startTime = 2.0f;
    float _timeRate = 1.0f;

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
        CheckWinCondition();
        GameOver();
        DisplayHeath();
        DisplayAmmo();
    }

    void GameOver()
    {
        if (_baseScript.Heath == 0)
        {
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

    void SpawnEnemy()
    {
        if (_enemiesSpawned <= _maximumEnemies)
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
            _youWin.SetActive(true);
        }
    }
}
