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
        Destroy(zomb.gameObject);
    }

    public void SpawnZombie()
    {
        var zombie = Instantiate(prefab);
        var rnd = Random.Range(0, spawners.Count);

        zombie.transform.position = spawners[rnd].position;
        zombie.GetComponent<MoveTo>().destination = spawners[rnd].parent.parent.parent.position;
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
