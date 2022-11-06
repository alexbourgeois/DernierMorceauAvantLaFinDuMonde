using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float RotPerMinute;
    public GameObject ToRotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ToRotate.transform.Rotate(-RotPerMinute * Time.deltaTime, 0, 0, Space.World);
    }
}
