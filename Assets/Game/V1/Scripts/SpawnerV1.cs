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
    // Start is called before the first frame update
    void Awake()
    {

      //  instance = this;
    }

    public void KillZombie(ZombieInteraction zomb)
    {
        zombies.Remove(zomb);
        zomb.GetComponent<Renderer>().material.color = Color.blue;
        StartCoroutine(Tools.DelayAction(0.25f, () => Destroy(zomb.gameObject)));
        
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

        zombie.transform.parent = zombieAnchor;

        zombies.Add(interact);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
