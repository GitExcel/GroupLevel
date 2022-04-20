using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerapivotscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public float rotx = 0;
    float roty = 0;
    float rotz = 0;

    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        float horizontal = Input.GetAxis("HorizontalCamera");
        if (horizontal > 0)
        {
            roty = roty + horizontal;
        }
        else if (horizontal < 0)
        {
            roty = roty + horizontal;
        }

        float vertical = Input.GetAxis("VerticalCamera");
        if (vertical > 0)
        {
            rotx = rotx + vertical;
        }
        else if (vertical < 0)
        {
            rotx = rotx + vertical;
        }


        transform.rotation = Quaternion.Euler(rotx, roty, rotz);
    }
}
