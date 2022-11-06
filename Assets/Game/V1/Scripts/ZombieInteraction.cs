using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInteraction : MonoBehaviour
{
    public int trackIndex = -1;
    public bool isLeft = false;
    public bool canBeKilled = false;
    public bool isDead = false;
    public SpawnerV1 spawner;
    public Collider leftCollider;
    public Collider rightCollider;

    public float soundProba = 0.1f;
    public float soundDelay = 1.0f;
    private float _previousTime;
    private void Update()
    {
        if(Time.time - _previousTime >= soundDelay)
        {
            _previousTime = Time.time;
            var rnd = Random.Range(0.0f, 1.0f);
            if(rnd <= soundProba)
                AudioManager.instance.PlayZombieLifeSound();
        }

    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider == leftCollider || collider == rightCollider)
        {
            canBeKilled = true;
        }

        if(collider == Areas.instance.damageArea && !isDead)
        {
            PlayerInfo.instance.LooseLife();
            spawner.KillZombie(this, 0.0f);
        }
    }


}
