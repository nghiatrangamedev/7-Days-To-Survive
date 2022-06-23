using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _heathText;
    [SerializeField] TextMeshProUGUI _ammoText;
    [SerializeField] GameObject _base;

    Base _baseScript;
    PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _baseScript = _base.GetComponent<Base>();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
        DisplayHeath();
        DisplayAmmo();
    }

    void GameOver()
    {
        if (_baseScript.Heath == 0)
        {
            Destroy(_base);
            Debug.Log("Game Over");
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
}
