using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorbuttontriggerscript : MonoBehaviour
{
    public float firstcamduration = 0;
    public float secondcamduration = 0;
    public Camera camera1;
    public Camera camera2; // set this to the first camera if second camera is not used
    public Camera playercamera;
    public GameObject player;
    public playerctrl playerscript;
    public bool activated = false;
    public bool buttonmode = true;
    private bool buttonmodeactive = false;
    public bool cutscene = false;
    private bool cutscenebool = true;
    public bool cutscenerepeatable = false;

    public Animator animator;

    private void Start()
    {
        GetComponent<Animator>();
        camera1.enabled = false;
        camera2.enabled = false;
    }

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
        buttonmodeactive = false;
    }
    private void Update()
    {
        if (buttonmodeactive)
        {
            if (Input.GetButtonDown("Use"))
            {
                if (!activated)
                {
                    activated = true;
                }
                else if (activated)
                {
                    activated = false;
                }

                if (cutscene)
                {
                    if (cutscenebool)
                    {

                        StartCoroutine(rout1());
                        StartCoroutine(rout2());
                        cutscenebool = false;
                        //do cutscene
                    }

                }

            }
        }
    }

    // disables controls
    IEnumerator rout1()
    {
        yield return new WaitForSeconds(0.01f);
        playercamera.enabled = false;
        camera1.enabled = true;
        //player.transform.rotation = transform.rotation; //sets the player in the direction of the trigger, which should ideally be facing the button
        animator.SetBool("ismoving", false); // stops the player from defaulting to the running animation in the cutscene
        animator.SetTrigger("punch");
        playerscript.GetComponent<playerctrl>().enabled = false;
    }


    // enables controls again
    IEnumerator rout2()
    {
        yield return new WaitForSeconds(firstcamduration);
        camera1.enabled = false;
        camera2.enabled = true;
        yield return new WaitForSeconds(secondcamduration);
        camera2.enabled = false;
        playercamera.enabled = true;
        playerscript.GetComponent<playerctrl>().enabled = true;
        cutscenebool = true;
    }
}
