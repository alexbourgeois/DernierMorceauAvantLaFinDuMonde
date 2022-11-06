using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public GameObject originSpawn;
    public GameObject Ground;
    public GameObject Parent;
    public GameObject SpawnZone_01;
    public GameObject SpawnZone_02;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "ground")
        {
            Debug.Log("<color=yellow>hit</color>");
           var myNewGround = Instantiate(Ground, originSpawn.transform.position, Quaternion.Euler(0,90,0));

            myNewGround.transform.parent = Parent.transform;
            SpawnZone_01.GetComponent<SpawnRandomActor>().SpawnFood();
            SpawnZone_02.GetComponent<SpawnRandomActor>().SpawnFood();
        }
    }
}
