using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punchdetect : MonoBehaviour
{
    public playerctrl playerref;
    public GameObject enemy = null;
    bool enemyinrange = false;
    // Start is called before the first frame update
    void Start()
    {
        enemy = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyinrange = true;
            enemy = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyinrange = false;
            enemy = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
