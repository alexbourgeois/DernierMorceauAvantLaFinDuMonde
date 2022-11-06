using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomActor : MonoBehaviour
{
    public int numProps;
    public GameObject[] props;
    public GameObject FoodPrefab;
    public GameObject Parent;
    public bool SpawnAtStart = false;
    public bool SpawnOnCollision = false;


    public Vector3 center;
    public Vector3 size;
    public Quaternion min;
    public Quaternion max;
    // Update is called once per frame

    void Start()
    {
        if (SpawnAtStart == true)
        {


            for (int i = 0; i < numProps; i++)
            {
                SpawnFood();
            }
        }
    }

    void Update()
    {

    }
    public void SpawnFood()
    {
        int randomIndex = Random.Range(0, props.Length);
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        var myNewProp = Instantiate(props[randomIndex], pos, Quaternion.identity);

        myNewProp.transform.parent = Parent.transform;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

}
