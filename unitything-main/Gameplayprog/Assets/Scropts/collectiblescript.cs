using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectiblescript : MonoBehaviour
{

    public float respawntime = 30;

    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        Invoke("respawn", respawntime);
    }

    private void Update()
    {
        
    }

    void respawn()
    {
        gameObject.SetActive(true);
    }
}
