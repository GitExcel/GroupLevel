using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera2Dscript : MonoBehaviour
{
    public playerctrl playerscript;
    public GameObject player;
    //public GameObject leftcampos;
    //public GameObject rightcampos;
    //public GameObject maincamlocation;

    Vector3 startpos;
    Quaternion startrot;
    public bool left;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startpos = transform.localPosition;
        Quaternion startrot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerscript.twodimension)
        {
            if (left)
            {
                //transform.position = leftcampos.transform.position;
                //transform.rotation = leftcampos.transform.rotation;
            }
            else if (left == false)
            {
                //transform.position = rightcampos.transform.position;
                //transform.rotation = rightcampos.transform.rotation;
            }
        }
        else
        {
            //transform.position = maincamlocation.transform.position;
            //transform.rotation = maincamlocation.transform.rotation; ;
        }
        
    }

    void lerpleft()
    {

    }

    void lerpright()
    {

    }

    void comeback()
    {

    }
}
