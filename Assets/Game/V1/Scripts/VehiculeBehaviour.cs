using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiculeBehaviour : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().tag == "Obstacle")
        {
            PlayerInfo.instance.LooseLife(10);
        }

        if (collision.GetComponent<Collider>().tag == "Choice")
        {
            DoorManager.instance.ComputeChoice();
        }

        if (collision.GetComponent<Collider>().tag == "ScoreUp")
        {
            PlayerInfo.instance.WinLife(10);
        }
    }
}
