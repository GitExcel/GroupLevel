using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class playerctrl : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        doublejumpactive = false;
        GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked; // makes it so the cursor doesn't escape during gameplay
        targetlockcam.enabled = false;
        twodimensionalcam.enabled = false;
        
    }

    public bool lockedon = false;

    public GameObject theguy;
    public GameObject magicarrow;
    public magicarrow magicarrowscript;
    public GameObject Speedparticle;
    public GameObject Cloudparticle;
    public GameObject Powerparticle;
    public GameObject respawnpoint;
    

    public CinemachineFreeLook normalcam;
    public CinemachineVirtualCamera pretendcam;
    public Camera targetlockcam;
    public Camera twodimensionalcam;
    public CinemachineVirtualCamera pretendcam2D;
    //woog


    private float speedtimer = 10;
    private float jumptimer = 15;
    private float powertimer = 10;


    public int phealth = 1000;
    public CharacterController playercontrol;
    public Transform camera;
    public float speed = 10f;
    public float smoothspeed = 20.0f;
    float turnvelocity;
    Vector3 jumpvelocity;
    public bool incutscene = false;
    public float gravity = 9.8f;
    public float jumpheight = 2f;
    public bool readytojump;
    public bool readytodoublejump;
    public bool doublejumpactive = false;
    public bool doublejumppowerupactive = false;
    public bool speedactive = false;
    public bool poweractive = false;
    public bool rollable = true;
    public bool rolling = false;
    private bool punchable = true;
    public bool twodimension = false;
    public bool touching2D = false;
    public bool punchhit = false;

    public int punchdamage = 1;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(phealth);
        if (incutscene == false)
        {
            if (playercontrol.isGrounded && jumpvelocity.y < 0)
            {
                jumpvelocity.y = 0f;
                readytojump = true;
                readytodoublejump = true;
                animator.SetBool("isgrounded", true);
                animator.SetBool("isairborne", false);
                animator.SetBool("isfalling", false);
            }


            if (jumpvelocity.y < -1)
            {
                animator.SetBool("isgrounded", false);
                animator.SetBool("isairborne", true);
                animator.SetBool("isfalling", true);
                if (readytojump == true)
                {
                    readytojump = false;
                }
            }

            
            float horiz = Input.GetAxisRaw("Horizontal");
            float vert = Input.GetAxisRaw("Vertical");
            // this part below makes the character look in the right direction while entering 2D. I tried a LOT of stuff before this.
            if (twodimension)
            {
                vert = 0;
                if (magicarrowscript.continuemove)
                {
                    if (magicarrowscript.continuingFORWARD)
                    {
                        horiz = 1;
                    }    
                    else if (!magicarrowscript.continuingFORWARD)
                    {
                        horiz = -1;
                    }
                }
            }

            Vector3 direction = new Vector3(horiz, 0f, vert).normalized;
            if (direction.magnitude > 0.1f && !rolling)
            {
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
                float grangle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnvelocity, smoothspeed);
                transform.rotation = Quaternion.Euler(0f, grangle, 0f);

                Vector3 movedirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
                if (!twodimension)
                {
                    playercontrol.Move(movedirection * speed * Time.deltaTime);
                }
                

                animator.SetBool("ismoving", true);
            }
            else if (rolling)
            {
                Vector3 movedirection = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f) * Vector3.forward;
                playercontrol.Move(movedirection * speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("ismoving", false);
            }
            if (speedactive)
            {
                speed = 20f;
                animator.SetFloat("animspeed", 2);
                if (speedtimer >= 0)
                {
                    speedtimer -= Time.deltaTime;
                }
                else
                {
                    Speedparticle.GetComponent<ParticleSystem>().Stop();
                    speed = 10f;
                    animator.SetFloat("animspeed", 1);
                    speedactive = false;
                }
            }
            if (doublejumppowerupactive)
            {
                doublejumpactive = true;
                if (jumptimer >= 0)
                {
                    jumptimer -= Time.deltaTime;
                }
                else
                {
                
                    doublejumpactive = false;
                    Cloudparticle.GetComponent<ParticleSystem>().Stop();
                    doublejumppowerupactive = false;
                }
            }
            if (poweractive)
            {
                if (powertimer >= 0)
                {
                    powertimer -= Time.deltaTime;
                }
                else
                {
                    Powerparticle.GetComponent<ParticleSystem>().Stop();
                    punchdamage = 1;
                    poweractive = false;
                }
            }

            if (Input.GetButtonDown("Menu"))
            {
                Debug.Log("Menu");
                
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (readytojump)
                {
                    jumpvelocity.y = 8;
                    readytojump = false;
                    readytodoublejump = true;
                    animator.SetTrigger("jumped");
                    animator.SetBool("isairborne", true);
                }
                else if (readytodoublejump)
                {
                    if (doublejumpactive == true)
                    {
                        jumpvelocity.y = 8;
                        animator.SetTrigger("doublejumped");
                        readytodoublejump = false;
                    }
                }
            }

            if (!playercontrol.isGrounded)
            {
                jumpvelocity.y -= gravity * Time.deltaTime;
            }
            playercontrol.Move(jumpvelocity * Time.deltaTime);

            if (punchable)
            {
                if (Input.GetButtonDown("Attack"))
                {
                    animator.SetTrigger("punch");
                    punchable = false;
                    StartCoroutine(punchroutine());
                }
            }


            if (!twodimension)
            {
                if (Input.GetButtonDown("Lockon"))
                {
                    if (lockedon == false)
                    {
                        ActivateLockon();
                        lockedon = true;
                    }
                    else
                    {
                        DectivateLockon();
                        lockedon = false;
                    }

                }

                if (rollable)
                {
                    if (Input.GetButtonDown("Shmove"))
                    {
                        animator.SetTrigger("roll");
                        rollable = false;
                        rolling = true;
                        StartCoroutine(rollroutine());
                    }
                }
            }

            if (twodimension)
            {
                if (lockedon)
                {
                    DectivateLockon();
                    lockedon = false;
                }

                transform.localPosition = new Vector3(magicarrow.transform.position.x, transform.position.y, magicarrow.transform.position.z);
                twodimensionalcam.enabled = true;
                pretendcam2D.Priority = 12;
                normalcam.ForceCameraPosition(pretendcam2D.transform.position, pretendcam2D.transform.rotation); // THIS SMOOTHS OUT EXITING 2D
            }
            else
            {
                pretendcam2D.Priority = 4;
                twodimensionalcam.enabled = false;
            }
        }



        IEnumerator punchroutine()
    {
        yield return new WaitForSeconds(0.4f);
            Debug.Log("attack");
            punchhit = true;
        yield return new WaitForSeconds(0.1f);
            punchhit = false;
        yield return new WaitForSeconds(0.9f);
        punchable = true;
    }

    IEnumerator rollroutine()
    {
        yield return new WaitForSeconds(1);
        rollable = true;
            rolling = false;
    }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Speedcol")
        {
            speedactive = true;
            speedtimer = 10;
            Speedparticle.GetComponent<ParticleSystem>().Play();
        }
        if (collision.gameObject.tag == "Jumpcol")
        {
            doublejumppowerupactive = true;
            doublejumpactive = true;
            jumptimer = 15;
            Cloudparticle.GetComponent<ParticleSystem>().Play();
        }
        if (collision.gameObject.tag == "Powercol")
        {
            Powerparticle.GetComponent<ParticleSystem>().Play();
            powertimer = 10;
            punchdamage = 10;
            poweractive = true;
        }
        if (collision.gameObject.tag == "TwoDimEnter")
        {
            twodimension = true;
        }
        if (collision.gameObject.tag == "TwoDimExit")
        {
            twodimension = false;
        }
    }

    void ActivateLockon()
    {
        normalcam.Priority = 5;
        pretendcam.Priority = 10;
        targetlockcam.enabled = true;
    }

    void DectivateLockon()
    {
        normalcam.Priority = 10;
        pretendcam.Priority = 5;
        targetlockcam.enabled = false;
    }
}
