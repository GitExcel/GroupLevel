using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feet : MonoBehaviour
{
    public GameObject player;
    public AudioSource audiosauce;
    public AudioSource audiosauce2;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("PlayerControl");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Foot1()
    {
        playerctrl playerscript = player.GetComponent<playerctrl>();
        bool rolling = playerscript.GetComponent<playerctrl>().rolling;
        if (!rolling)
        {
            audiosauce = GetComponent<AudioSource>();
            audiosauce.Play(0);
        }
        
    }

    public void Foot2()
    {
        playerctrl playerscript = player.GetComponent<playerctrl>();
        bool rolling = playerscript.GetComponent<playerctrl>().rolling;
        if (!rolling)
        {
            audiosauce2 = GetComponent<AudioSource>();
            audiosauce2.Play(0);
        }
        
        
    }
}
