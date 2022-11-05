using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiculeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    }
}
