using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
  public GameObject Player;
  [SerializeField] private PortalScript target;
  [SerializeField] private Transform spawnPoint;
  public Transform spawnP{get {return spawnPoint;}}

  private void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      other.transform.position = target.spawnP.position;
    }
  }
}
