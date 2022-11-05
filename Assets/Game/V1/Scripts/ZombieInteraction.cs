using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInteraction : MonoBehaviour
{
    public int trackIndex = -1;
    public bool isLeft = false;
    public bool canBeKilled = false;
    public Collider leftCollider;
    public Collider rightCollider;

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
        if(collision.GetComponent<Collider>() == leftCollider || collision.GetComponent<Collider>() == rightCollider)
        {
            canBeKilled = true;
        }

        if(collision.GetComponent<Collider>() == Areas.instance.damageArea)
        {
            PlayerInfo.instance.LooseLife();
            Destroy(this.gameObject);
        }
    }


}
