using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class magicarrow : MonoBehaviour
{
    public Animator animator;
    public PathCreator pathCreatorTwo;
    public GameObject player;
    public playerctrl playerscript;
    public GameObject playerdetect;

    float distance = 0.1f;
    bool onetime = true;
    public bool continuemove = false;
    public bool continuemoveLEFT = false;
    public bool continuemoveUP = false;
    public bool continuemoveRIGHT = false;
    public bool continuemoveDOWN = false;
    public bool continuingFORWARD = false;
    float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pathCreatorTwo != null)
        {
            if (playerscript.twodimension)
            {
                float horiz = Input.GetAxisRaw("Horizontal");
                float vert = Input.GetAxisRaw("Vertical");
                if (onetime == true)
                {
                    distance = pathCreatorTwo.path.GetClosestDistanceAlongPath(player.transform.position);
                    onetime = false;
                    
                    if (horiz < -0.1)
                    {
                        continuemoveLEFT = true;
                        continuemove = true;
                    }
                    if (vert > 0.1)
                    {
                        continuemoveUP = true;
                        continuemove = true;
                    }
                    if (horiz > 0.1)
                    {
                        continuemoveRIGHT = true;
                        continuemove = true;
                    }
                    if (vert < -0.1)
                    {
                        continuemoveDOWN = true;
                        continuemove = true;
                    }
                }
                speed = playerscript.speed; // this is referenced from the player due to picking up powerups
                if (continuemove)
                {
                    animator.SetBool("ismoving", true);
                    float pathmeasure = pathCreatorTwo.path.GetClosestTimeOnPath(transform.position);
                    if (pathmeasure < 0.2)
                    {
                        continuingFORWARD = true; // used in the player script
                        //player.transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
                        distance += speed * Time.deltaTime;
                        transform.position = pathCreatorTwo.path.GetPointAtDistance(distance);
                    }
                    else if (pathmeasure > 0.8)
                    {
                        continuingFORWARD = false; // used in the player script
                        //player.transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
                        distance -= speed * Time.deltaTime;
                        transform.position = pathCreatorTwo.path.GetPointAtDistance(distance);
                    }
                    else
                    {
                        animator.SetBool("ismoving", false);
                        continuemove = false;
                        continuemoveUP = false;
                        continuemoveLEFT = false;
                        continuemoveRIGHT = false;
                        continuemoveDOWN = false;

                    }

                    if (continuemoveUP)
                    {
                        if (horiz > 0.1 || horiz < -0.1 || vert < 0.1)
                        {
                            continuemoveUP = false;
                            continuemoveLEFT = false;
                            continuemoveDOWN = false;
                            continuemoveRIGHT = false;
                            continuemove = false;
                        }
                    }

                    if (continuemoveLEFT)
                    {
                        if (horiz > -0.1)
                        {
                            continuemoveUP = false;
                            continuemoveLEFT = false;
                            continuemoveDOWN = false;
                            continuemoveRIGHT = false;
                            continuemove = false;
                        }
                    }

                    if (continuemoveDOWN)
                    {
                        if (horiz > 0.1 || horiz < -0.1 || vert > -0.1)
                        {
                            continuemoveUP = false;
                            continuemoveLEFT = false;
                            continuemoveDOWN = false;
                            continuemoveRIGHT = false;
                            continuemove = false;
                        }
                    }

                    if (continuemoveRIGHT)
                    {
                        if (horiz < 0.1)
                        {
                            continuemoveUP = false;
                            continuemoveLEFT = false;
                            continuemoveDOWN = false;
                            continuemoveRIGHT = false;
                            continuemove = false;
                        }
                    }
                }
                else if (horiz > 0.1)
                {
                    distance += speed * Time.deltaTime;
                    transform.position = pathCreatorTwo.path.GetPointAtDistance(distance);
                }
                else if (horiz < -0.1)
                {
                    distance -= speed * Time.deltaTime;
                    transform.position = pathCreatorTwo.path.GetPointAtDistance(distance);
                }
                


                transform.position = pathCreatorTwo.path.GetPointAtDistance(distance);
                transform.rotation = pathCreatorTwo.path.GetRotationAtDistance(distance);

                playerdetect.transform.position = player.transform.position;
            }
        } 
        else
        {
            Debug.Log(onetime);
            onetime = true;
        }
    }
}
