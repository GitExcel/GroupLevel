using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falltrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject respawnlocation;
    public bool why = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (why)
        {
            player.transform.position = respawnlocation.transform.position;
            Application.LoadLevel(Application.loadedLevel);
            why = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            why = true;
        }
    }
}
