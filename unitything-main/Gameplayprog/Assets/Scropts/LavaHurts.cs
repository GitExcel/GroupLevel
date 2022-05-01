using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHurts : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<playerctrl>().phealth -= 20;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
