using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLevelPortal : MonoBehaviour
{
  public string loadLevel;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.tag == "Player")
      {
        Application.LoadLevel(loadLevel);
      }
    }
}
