using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thetarget : MonoBehaviour
{

    public GameObject player;
    public GameObject target;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = 1000.0f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, t);
    }
}
