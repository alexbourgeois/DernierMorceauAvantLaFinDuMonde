using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

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
    public List<GameObject> audioTracks = new List<GameObject>();

    public Transform playerHolder;

    public bool gamePaused = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var track in audioTracks)
            track.SetActive(false);
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

        audioTracks[players.Count - 1].SetActive(true);
        var director = audioTracks[players.Count - 1].GetComponentInChildren<PlayableDirector>();
        director.Stop();
        director.time = Time.time;
        director.Play();

    }

    public void TogglePause()
    {
        if(gamePaused)
        {
            gamePaused = false;
            Time.timeScale = 0.0f;
        }
        else
        {
            gamePaused = true;
            Time.timeScale = 1.0f;
        }

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

    public void WinLife(int count)
    {
        life += count;
        lifeTxt.text = life.ToString();
    }

    public void WinLife()
    {
        life++;
        lifeTxt.text = life.ToString();
    }
}
