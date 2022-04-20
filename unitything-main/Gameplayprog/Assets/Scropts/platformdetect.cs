using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformdetect : MonoBehaviour
{
    public GameObject player;
    public GameObject platformroot;
    public bool touching = false;
    public bool nottouching = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (touching)
        {
            player.transform.parent = platformroot.transform;
            touching = false;
        }

        if (nottouching)
        {
            player.transform.parent = null;
            nottouching = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            touching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nottouching = true;
        }
    }
}
