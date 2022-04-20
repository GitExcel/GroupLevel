using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascropt : MonoBehaviour
{
    // Start is called before the first frame update


    private float longth = -5;

    void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (longth! > -1)
        {
            longth = -0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (longth < -5)
        {
            longth = -5;
        }

       if (longth > -5)
        {
            longth = -0.1f;
        }

        transform.localPosition = new Vector3(0, 0, longth);


    }
}
