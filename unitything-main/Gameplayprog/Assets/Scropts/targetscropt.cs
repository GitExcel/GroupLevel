using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetscropt : MonoBehaviour
{
    public GameObject player;
    bool enemyvalid = false;
    public GameObject[] targets;
    int enemysearchcooldown = 300;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (player.GetComponent<playerctrl>().lockedon)
        {
            GameObject[] targets;
            targets = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closestenemy = null;
            float distance = Mathf.Infinity;
            Vector3 position = player.transform.position;
            foreach (GameObject targ in targets)
            {
                Vector3 diff = targ.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestenemy = targ;
                    distance = curDistance;
                }
            }
            if (closestenemy != null)
            {
                transform.position = closestenemy.transform.position;
                enemyvalid = true;
            }
            else
            {
                enemyvalid = false;
            }
        }

        if (!enemyvalid)
        {
            player.GetComponent<playerctrl>().lockedon = false;
            player.GetComponent<playerctrl>().normalcam.Priority = 10;
            player.GetComponent<playerctrl>().pretendcam.Priority = 5;
            player.GetComponent<playerctrl>().targetlockcam.enabled = false;
        }    
    }
}
