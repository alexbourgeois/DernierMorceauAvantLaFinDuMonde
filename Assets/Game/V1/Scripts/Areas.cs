using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Areas : MonoBehaviour
{
    public static Areas instance;

    public BoxCollider damageArea;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
}
