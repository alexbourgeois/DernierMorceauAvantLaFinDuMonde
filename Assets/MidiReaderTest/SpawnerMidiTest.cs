using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMidiTest : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnZombie()
    {
        Instantiate(prefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
