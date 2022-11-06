using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    public delegate void OnPlayerCreated();
    public static event OnPlayerCreated onPlayerCreated;

    public List<Transform> playerAnchors;
    public List<Transform> tokenAnchors;
    public List<PlayerSounds> playerDatas;

    public int life = 10;
    public TMP_Text lifeTxt;

    public List<PlayerV1> players = new List<PlayerV1>();

    public Transform playerHolder;
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

    public void RegisterPlayer(PlayerV1 player)
    {
        //player.transform.parent = playerHolder;
        players.Add(player);
        onPlayerCreated?.Invoke();


    }

    public int GetVoteCountIndex()
    {
        var index = -1;
        var max = 0;
        int maxEncounter = 0;
        for (int j = 0; j < 3; j++)
        {
            var score = 0;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].actualPosIndex == j)
                    score++;
            }
            if (score >= max)
            {
                max = score;
                index = j;
            }
        }

        for (int j = 0; j < 3; j++) //Check tie
        {
            var score = 0;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].actualPosIndex == j)
                    score++;
            }
            if (score == max)
            {
                maxEncounter++;
                max = score;
            }
        }

        if (maxEncounter > 1)
            index = -1;

        Debug.Log("Voted index : " + index);
        return index;
    }

    public void LooseLife(int count)
    {
        life -= count;
        lifeTxt.text = life.ToString();
    }

    public void LooseLife()
    {
        life--;
        lifeTxt.text = life.ToString();
    }
}
