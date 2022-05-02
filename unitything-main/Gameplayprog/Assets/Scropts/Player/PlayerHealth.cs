using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 100;
    public static bool IsPlayerDead = false;
    
    //private int range = 100;
    private GameObject enemy;
    public int damage = 20;
    private Transform target;
    
    private Animator anim;
    private GameObject sword;

    public Image imgHealthBar;
    
    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        anim = GetComponent<Animator>();
        sword = GameObject.FindGameObjectWithTag("Sword");
    }

    private void Update()
    {
        if (Input.GetButton("Attack"))
        {
            Debug.Log("Attacking");
            StartCoroutine(Attack());
            //AttackCheck();
        }
    }
    
    // void AttackCheck()
    // {
    //     Ray rayFrom = new Ray(transform.position, transform.forward); // attach sword to cam
    //     RaycastHit hit;
    //
    //     if (Physics.Raycast(rayFrom, out hit, range))
    //     {
    //         if (hit.collider.CompareTag("Enemy"))
    //         {
    //             Debug.Log("HIT: " + damage);
    //             hit.collider.GetComponent<EnemyHealthAttack>().TakeDamage(damage);
    //         }
    //         else 
    //         {
    //             Debug.Log("MISS");
    //         }
    //     }
    // }
    
    public void PlayerTakeDamage(int damage)
    {
        Debug.Log("TAKES DAMAGE");
        currentHealth = currentHealth - damage;
        
        if (currentHealth <= 0)
        {
            Debug.Log("Player Dead: " + currentHealth);
            gameObject.GetComponent<Animator>().Play("Die");
        }
        imgHealthBar.fillAmount = imgHealthBar.fillAmount - (currentHealth * 0.001f);

        
    }
    
    private IEnumerator Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
    }
}
