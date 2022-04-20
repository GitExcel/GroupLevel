using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class platform : MonoBehaviour
{
    public PathCreator pathCreator;
    float distance = 0.0f;
    public bool platformActive = false;
    public float startPoint = 0.0f;
    public float speed = 3;
    bool doonce = true;
    public doorbuttontriggerscript buttonref;
    public bool buttonmode = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (doonce)
        {
            transform.transform.position = pathCreator.path.GetPointAtTime(startPoint);
            distance = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distance);
            doonce = false;
        }

        if (buttonmode)
        {
            if (buttonref.activated)
            {
                platformActive = true;
            }
            else if (!buttonref.activated)
            {
                platformActive = false;
            }
        }
          

        if (platformActive)
        {
            distance += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distance);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distance);
        }
    }
}
