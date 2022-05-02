using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolAndAttack : MonoBehaviour
{
    public float speed;
    public float gravity;
    
    public Transform[] moveToSpots;
    private int moveToSpotIndex;
    private float distanceToSpot;
    public float rotSpeed;
    
    private float waitTime;
    public float startWaitTime;
    
    private GameObject player;
    public bool playerInRange = false;
    private float distanceToPlayer;
    private Transform target;
    
    private bool canAttack = true;
    public float enemyCooldown = 1;

    //int count;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        moveToSpotIndex = 0;
        waitTime = startWaitTime;
        moveToSpotIndex = Random.Range(0, moveToSpots.Length);
        
        target = player.transform;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy spotted the player");
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

   

    void Move(float speed)
    {
        float step =  speed * Time.deltaTime;
        var enemyPos = gameObject.transform.position;
        var playerPos = player.transform.position;
        
        Quaternion targetRotation = Quaternion.LookRotation(playerPos - enemyPos);

        transform.position = Vector3.MoveTowards(enemyPos, playerPos, step);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }

    void Update()
    {
        //var position = gameObject.transform.position;
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, target.position);
        if (EnemyHealth.IsEnemyDead == false)
        {
            if (playerInRange && canAttack)
            {
                Move(speed+3);
                if (distanceToPlayer < 0.9f)
                {
                    StartCoroutine(AttackCooldown());
                }
            }
            else if (canAttack && !playerInRange)
            {
                Patrolling();
            }
            else if (!canAttack)
            {
                transform.position += Vector3.back * speed * Time.deltaTime;
            }
        }
    }

    private void SpottingThePlayer()
    {
        // var position = gameObject.transform.position;
        // distanceToPlayer = Vector3.Distance(position, target.position);
        // if (EnemyHealthAttack.IsEnemyDead == false)
        // {
        //     if (playerInRange && canAttack)
        //     {
        //         Move(speed);
        //         if (distanceToPlayer < 1f)
        //         {
        //             gameObject.GetComponent<EnemyHealthAttack>().AttackPlayer();
        //             //StartCoroutine(AttackCooldown());
        //             //Debug.Log("distance is very close ngl");
        //         }
        //     }
        //     else
        //     {
        //         Patrolling();
        //     }
        // }
        //
    }
    
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
    }

    void Patrolling()
    {
        //Debug.Log("Patrolling");
        var position = gameObject.transform.position;
        
        Quaternion targetRotation = Quaternion.LookRotation(moveToSpots[moveToSpotIndex].position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        
        transform.position = Vector3.MoveTowards(position, moveToSpots[moveToSpotIndex].position,
            speed*Time.deltaTime);
        
        distanceToSpot = Vector3.Distance(position, moveToSpots[moveToSpotIndex].position);
        if (distanceToSpot < 0.5f)
        {
            if (waitTime <= 0)
            {
                waitTime = startWaitTime;
                moveToSpotIndex = Random.Range(0, moveToSpots.Length); // pick a new one
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
