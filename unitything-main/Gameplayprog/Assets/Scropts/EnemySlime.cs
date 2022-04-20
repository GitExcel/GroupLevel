using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.AI;

public class EnemySlime : MonoBehaviour
{
    public Material slimemat;
    public Material slimehurtmat;
    NavMeshAgent navmesh;
    public GameObject smallerslimeprefab;

    public GameObject enemyzonetrigger;
    public PathCreator pathCreator;
    float distance = 0.0f;
    public float patrolspeed = 3;

    public bool patrolling = true;

    public bool aggro = false;

    Vector3 returnpos;

    public GameObject punchref;
    public GameObject player;

    public int health = 3;
    bool invuln = false;
    bool slimeswitch = true;
    public int iframecount = 60;
    int iframes;
    public int slimesize = 3; // 3 = big, 2 = medium, 1 = small
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        punchref = GameObject.FindWithTag("punchdetection");
        player = GameObject.FindWithTag("Player");
        iframes = iframecount;
        navmesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyzonetrigger.GetComponent<enemytrigger>().activated)
        {
            if (patrolling)
            {
                returnpos = transform.position;
            }    
            patrolling = false;
            aggro = true;
        }
        else
        {
            aggro = false;
        }


        if (aggro)
        {
            navmesh.destination = player.transform.position;
            patrolling = false;

            if (invuln)
            {
                navmesh.destination = transform.position; // stops the slime moving when recovering from damage
                slimeswitch = true;
                GetComponent<Renderer>().material = slimehurtmat;
                if (iframes > 0)
                {
                    iframes--;
                }
                else
                {
                    invuln = false;
                    iframes = iframecount;
                }
            }
            else
            {
                if (slimeswitch)
                {
                    GetComponent<Renderer>().material = slimemat;
                    slimeswitch = false;
                    if (health < 1)
                    {
                        if (slimesize != 1)
                        {
                            GameObject newcube1 = Instantiate(smallerslimeprefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), Quaternion.identity);
                            GameObject newcube2 = Instantiate(smallerslimeprefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), Quaternion.identity);

                            newcube1.GetComponent<EnemySlime>().pathCreator = pathCreator;
                            newcube1.GetComponent<EnemySlime>().returnpos = returnpos;
                            newcube1.GetComponent<EnemySlime>().distance = distance;
                            newcube1.GetComponent<EnemySlime>().enemyzonetrigger = enemyzonetrigger;

                            newcube2.GetComponent<EnemySlime>().pathCreator = pathCreator;
                            newcube2.GetComponent<EnemySlime>().returnpos = returnpos;
                            newcube2.GetComponent<EnemySlime>().distance = distance;
                            newcube2.GetComponent<EnemySlime>().enemyzonetrigger = enemyzonetrigger;

                        }
                        Destroy(gameObject);
                    }
                }
            }
            Debug.Log(health);
            if (punchref.GetComponent<punchdetect>().enemy == gameObject)
            {
                if (punchref.GetComponent<punchdetect>().playerref.punchhit)
                {
                    if (!invuln)
                    {
                        health = health - punchref.GetComponent<punchdetect>().playerref.punchdamage;
                        invuln = true;
                    }
                }
            }
        }
        else
        {
            if (!patrolling)
            {
                navmesh.destination = returnpos;
                float distanceToTarget = Vector3.Distance(transform.position, returnpos);
                if (distanceToTarget < 1)
                {
                    patrolling = true;
                }
            }
            else
            {
                distance += patrolspeed * Time.deltaTime;
                transform.position = new Vector3(pathCreator.path.GetPointAtDistance(distance).x, transform.position.y, pathCreator.path.GetPointAtDistance(distance).z);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distance);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (slimesize == 3)
            {
                player.GetComponent<playerctrl>().phealth = (player.GetComponent<playerctrl>().phealth - 5);
            }
            else if (slimesize == 2)
            {
                player.GetComponent<playerctrl>().phealth = (player.GetComponent<playerctrl>().phealth - 2);
            }
            else if (slimesize == 1)
            {
                player.GetComponent<playerctrl>().phealth = (player.GetComponent<playerctrl>().phealth - 1);
            }

        }
    }
}
