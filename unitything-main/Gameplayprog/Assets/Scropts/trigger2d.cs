using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class trigger2d : MonoBehaviour
{
    public magicarrow arrowscript;
    public bool activated = false;
    public PathCreator pathCreator;
    public GameObject arrow;
    public bool isExit = false;
    public float entryPoint = 0.0f;
    public playerctrl playerscript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!playerscript.twodimension)
        {
            if (other.tag == "Player")
            {
                activated = true;
                if (!isExit)
                {
                    arrowscript.pathCreatorTwo = pathCreator;
                    arrow.transform.position = pathCreator.path.GetPointAtTime(entryPoint);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activated = false;
            if (isExit)
            {
                arrowscript.pathCreatorTwo = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
