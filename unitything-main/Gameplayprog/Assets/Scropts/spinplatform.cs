using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinplatform : MonoBehaviour
{
    public platformdetect detectref;
    public GameObject platformmesh;
    float spinvalue = 0.0001f;
    bool SPIN = false;
    bool grog = false;
    float t = 0;
    Vector3 startscale;
    Vector3 tempscale;
    public float disappeartime = 5;
    public float reappeartime = 1;


    // Start is called before the first frame update
    void Start()
    {
        startscale = platformmesh.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinvalue, 0, 0);
        if (detectref.touching)
        {
            SPIN = true;
        }
        if (detectref.nottouching)
        {
            SPIN = false;
        }

        if (SPIN)
        {
            if (!grog)
            {
                spinvalue = 1f;
                grog = true;
                t = 0;
            }
            
            spinvalue = spinvalue + 1.5f * Time.deltaTime;
            
            t += Time.deltaTime / disappeartime;
            platformmesh.transform.localScale = Vector3.Lerp(startscale, new Vector3(0,0,0), t);
        }
        else if (!SPIN)
        {
            grog = false;

            t -= Time.deltaTime / reappeartime;
            platformmesh.transform.localScale = Vector3.Lerp(startscale, new Vector3(0, 0, 0), t);

            if (spinvalue < 0.1)
            {
                spinvalue = 0f;
            }
            else
            {
                spinvalue = spinvalue - 1 * Time.deltaTime;
            }
            

        }

    }
}
