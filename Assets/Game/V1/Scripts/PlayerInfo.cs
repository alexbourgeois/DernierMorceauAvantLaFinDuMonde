using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    public List<Transform> playerAnchors;
    public int life = 10;
    public TMP_Text lifeTxt;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LooseLife()
    {
        life--;
        lifeTxt.text = life.ToString();
    }
}
