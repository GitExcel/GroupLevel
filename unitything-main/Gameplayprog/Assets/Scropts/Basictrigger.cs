using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basictrigger : MonoBehaviour
{
    public bool activated = false;
    public bool buttonmode = false;
    private bool buttonmodeactive = false;

    void OnTriggerEnter(Collider other)
    {
        if (!buttonmode)
        {
            activated = true;
        }
        else
        {
            buttonmodeactive = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        activated = false;
        buttonmodeactive = false;
    }
    private void Update()
    {
        if (buttonmodeactive)
        {
            if (Input.GetButtonDown("Use"))
            {
                activated = true;
            }
        }
    }
}
