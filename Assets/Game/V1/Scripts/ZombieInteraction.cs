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

    private void OnTriggerEnter(Collider collider)
    {
        if(collider == leftCollider || collider == rightCollider)
        {
            canBeKilled = true;
        }

        if(collider == Areas.instance.damageArea)
        {
            PlayerInfo.instance.LooseLife();
            Destroy(this.gameObject);
        }
    }


}
