using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KillZone")
        {
        //Debug.Log("<color=yellow>hit</color>");
        Destroy(gameObject);

        }
    }
}
