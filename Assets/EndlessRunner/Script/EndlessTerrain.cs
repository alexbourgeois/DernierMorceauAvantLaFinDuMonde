using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{
    public float moveSpeed = 3;
    public GameObject targetObject;
    public GameObject Killzone;
   /* public GameObject SpawnZone01;
    public GameObject SpawnZone02;*/

  


    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetObject.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * -1, Space.World);
     
    }

    
}
