using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    float _heath = 100.0f;

    public float Heath
    {
        private set
        {
            if (value < 0)
            {
                value = 0;
            }

            else if (value > 100)
            {
                value = 100;
            }

            _heath = value;
        }
        get { return _heath; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hand")
        {
            Heath -= 50;
        }
    }

}
