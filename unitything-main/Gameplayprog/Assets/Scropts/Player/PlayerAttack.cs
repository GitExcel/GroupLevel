using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject enemy;
    private int damage;


    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        gameObject.GetComponent<playerctrl>().punchdamage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
