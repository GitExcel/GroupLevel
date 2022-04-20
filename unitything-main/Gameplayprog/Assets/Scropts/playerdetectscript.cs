using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerdetectscript : MonoBehaviour
{
    public GameObject player;
    public bool collid = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Untagged")
        {
            collid = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Untagged")
        {
            collid = false;
        }
    }
}
