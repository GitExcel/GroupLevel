using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int speed;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<playerctrl>().phealth += health;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, speed*Time.deltaTime, 0);
        
    }
}
