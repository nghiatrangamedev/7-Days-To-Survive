using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _base;
    Base _baseScript;

    // Start is called before the first frame update
    void Start()
    {
        _baseScript = _base.GetComponent<Base>();
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    void GameOver()
    {
        if (_baseScript.Heath == 0)
        {
            Destroy(_base);
            Debug.Log("Game Over");
        }
    }
}
