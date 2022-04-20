using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytrigger : MonoBehaviour
{
    public bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            activated = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activated = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
