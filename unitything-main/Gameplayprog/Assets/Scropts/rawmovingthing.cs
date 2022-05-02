using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rawmovingthing : MonoBehaviour
{
    public GameObject destination;
    private Vector3 startpos;
    private Vector3 endpos;
    private Quaternion startrot;
    private Quaternion endrot;
    private Vector3 startscale;
    private Vector3 endscale;
    public Basictrigger triggah;
    public bool directlymove = false;

    public float movetime = 0;
    float t;

    public bool scalebool = false;
    public bool deleteoncomplete = false;
    private bool movecompleted = false;
    private bool returncompleted = true;
    public bool forcereturn = false;

    public bool returnbool = true;

    private bool moving = false;
    private bool movingback = false;

    public float delaytime = 0;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        startrot = transform.rotation;
        startscale = transform.localScale;
        endpos = destination.transform.position;
        endrot = destination.transform.rotation;
        endscale = destination.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggah.activated || directlymove)
        {
            if (returncompleted)
            {
                returncompleted = false;
                Invoke("Moveblock", delaytime);
            }
        }
        else if (!triggah.activated)
        {
            if (movecompleted)
            {
                if (returnbool)
                {
                    movecompleted = false;
                    Invoke("Moveblockback", delaytime);
                }


            }
        }

        if (movecompleted)
        {
            if (!returnbool)
            {
                if (deleteoncomplete)
                {
                    Destroy(gameObject);
                }
            }
        }

        if (moving)
        {
            if (transform.position == endpos && transform.rotation == endrot)
            {
                t = 0;
                moving = false;
                movecompleted = true;
            }
            else
            {
                t += Time.deltaTime / movetime;
                transform.position = Vector3.Lerp(startpos, endpos, t);
                transform.rotation = Quaternion.Lerp(startrot, endrot, t);
                if (scalebool)
                {
                    transform.localScale = Vector3.Lerp(startscale, endscale, t);
                }
            }
        }

        if (movingback)
        {
            if (transform.position == startpos && transform.rotation == startrot)
            {
                t = 0;
                movingback = false;
                returncompleted = true;
            }
            else
            {
                t += Time.deltaTime / movetime;
                transform.position = Vector3.Lerp(endpos, startpos, t);
                transform.rotation = Quaternion.Lerp(endrot, startrot, t);
                if (scalebool)
                {
                    transform.localScale = Vector3.Lerp(endscale, startscale, t);
                }

            }
        }
    }

    void Moveblock()
    {
        moving = true;
    }

    void Moveblockback()
    {
        movingback = true;
    }
}