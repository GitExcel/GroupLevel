using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 1000;
    public static bool IsEnemyDead = false;
    
    private int range = 50;
    private bool canAttackPlayer = true;
    private GameObject player;
    private GameObject eyes;
    public int damage;
    private Transform target;

    public GameObject newPrefab;
    public GameObject newPrefab2;
    
    private Vector3 scale1;
    private Vector3 scale2;
    private Vector3 scale3;

    public EnemyHealthBar healthBar;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        eyes = GameObject.FindGameObjectWithTag("Eyes");
        
        scale2 = new Vector3(0.5f, 0.5f, 0.5f);
        scale3 = new Vector3(0.25f, 0.25f, 0.25f);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0 && IsEnemyDead == false)
        {
            Debug.Log("Dead: " + currentHealth);
            IsEnemyDead = true;
        }
        
        // if (currentHealth == 500)
        // {
        //     //Destroy(gameObject);
        //     Instantiate(newPrefab, transform.position, transform.rotation);
        //     newPrefab.transform.localScale = scale2;
        // }
        // else if (currentHealth == 250)
        // {
        //     //Destroy(newPrefab);
        //     Instantiate(newPrefab2, transform.position, transform.rotation);
        //     newPrefab2.transform.localScale = scale3;
        // }
        
        if (currentHealth <= 0)
        {
            IsEnemyDead = true;
            //Destroy(gameObject);
        }
    }


    public void Update()
    {
        Debug.Log(IsEnemyDead);
        if (currentHealth > 0 && IsEnemyDead == false) //&& PatrolAndAttack.playerInRange) 
        {
            AttackPlayer();
        }
        // if (IsEnemyDead)
        // {
        //     Debug.Log("ENEMY DED");
        //    // Destroy(gameObject);
        // }
    }

    public void AttackPlayer()
    {
        //Debug.Log("Attacking the player");
        Ray rayFrom = new Ray(eyes.transform.position, eyes.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(rayFrom, out hit, range))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (canAttackPlayer)
                {
                    StartCoroutine(Attacking());
                }
            }
        }
    }
    
    IEnumerator Attacking()
    {
        canAttackPlayer = false;
        player.GetComponent<playerctrl>().phealth -= damage;
        yield return new WaitForSeconds(0.9f); // how often enemy attacks
        canAttackPlayer = true;
    }
}
