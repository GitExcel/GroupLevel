using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float speed;

    public GameObject fumocounter;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, speed*Time.deltaTime, 0);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fumocounter.gameObject.GetComponent<FumoCount>().fumocount2 += 1;
            gameObject.SetActive(false);
            
        }
    }
}
