using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject enemy;
    public int damage = 100;
    public bool workplease;
    private bool bool1 = true;
    public GameObject playerreference;


    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            workplease = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            workplease = false;
        }
    }

    private void Update()
    {
        if (workplease)
        {
            if (playerreference.GetComponent<playerctrl>().punchhit)
            {
                if (bool1)
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
                    bool1 = false;
                }
            }
            else
            {
                bool1 = true;
            }
        }
        
    }
}
