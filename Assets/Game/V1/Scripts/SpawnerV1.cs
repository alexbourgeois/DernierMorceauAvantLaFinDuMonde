using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerV1 : MonoBehaviour
{
   // public static SpawnerV1 instance;
    public GameObject prefab;

    public List<Transform> spawners;
    public List<ZombieInteraction> zombies = new List<ZombieInteraction>();
    public Transform zombieAnchor;

    public bool useLeft;

    public Collider leftCollider;
    public Collider rightCollider;


    public void KillZombie(ZombieInteraction zomb, float delay = 0.2f, PlayerV1 player = null)
    {
        if (zomb != null)
        {
            if (zombies.Contains(zomb)) { 
                if(delay > 0.0f)
                {
                    AudioManager.instance.PlayZombieDeathSound();
                    PlayerInfo.instance.WinLife();
                }

                
                var zombMaterials = zomb.GetComponentInChildren<Renderer>().materials;
                foreach (var mat in zombMaterials)
                {
                    var col = Color.white;
                    if (player != null)
                        col = player.playerColor;

                    mat.SetColor("_EmissionColor", col * 2.0f);// = Color.blue;
                }

                zomb.isDead = true;
                StartCoroutine(Tools.DelayAction(delay, () => Destroy(zomb.gameObject)));
                zombies.Remove(zomb);
             }
        }
    }

    public void SpawnZombie()
    {
        var zombie = Instantiate(prefab);
        var rnd = Random.Range(0, spawners.Count);
        if (useLeft)
            rnd = 0;
        else
            rnd = 1;
        var spawner = spawners[rnd];

        zombie.transform.position = spawner.position;
        zombie.GetComponent<MoveTo>().destination = spawner.parent.parent.parent;

        var interact = zombie.GetComponent<ZombieInteraction>();
        interact.trackIndex = rnd;
        interact.leftCollider = leftCollider;
        interact.rightCollider = rightCollider;
        interact.isLeft = rnd == 0 ? true : false;
        interact.spawner = this;
            
        zombie.transform.parent = zombieAnchor;

        zombies.Add(interact);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
