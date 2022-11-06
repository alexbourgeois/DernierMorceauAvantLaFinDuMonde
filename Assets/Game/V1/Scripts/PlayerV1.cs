using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerV1 : MonoBehaviour
{
    public string playerName;
    public int playerNumber;
    private PlayerInputManager _playerInputManager;
    private bool _initialized;

    public int actualPosIndex = 1;
    public SpawnerV1 leftSpawner;
    public SpawnerV1 rightSpawner;
    public Color playerColor;
    
    public GameObject token;
    public GameObject sphere;

    public PlayerSounds playerData;
    public AudioSource playerSound;
    // Start is called before the first frame update
    void Start()
    {
        _playerInputManager = FindObjectOfType<PlayerInputManager>();

        actualPosIndex = _playerInputManager.playerCount - 1;
        playerNumber = actualPosIndex;

        playerData = PlayerInfo.instance.playerDatas[actualPosIndex];
        playerName = PlayerInfo.instance.playerDatas[actualPosIndex].playerName;
        playerColor = PlayerInfo.instance.playerDatas[actualPosIndex].playerColor;
        Debug.Log("Creating " + playerName);

        transform.position = PlayerInfo.instance.playerAnchors[actualPosIndex].transform.position + Vector3.up * 0.5f;
        leftSpawner = PlayerInfo.instance.playerAnchors[actualPosIndex].GetComponentsInChildren<SpawnerV1>()[0];
        rightSpawner = PlayerInfo.instance.playerAnchors[actualPosIndex].GetComponentsInChildren<SpawnerV1>()[1];

        sphere.GetComponent<Renderer>().material.color = playerColor;

        PlayerInfo.instance.RegisterPlayer(this);

        CreateToken();
        _initialized = true;
    }

    public void CreateToken()
    {
        token.transform.position = PlayerInfo.instance.tokenAnchors[actualPosIndex].position + Vector3.forward * 2.0f * playerNumber;
        token.GetComponentInChildren<Renderer>().material.color = playerColor;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayA()
    {
        if (!_initialized)
            return;

        ZombieInteraction zombToKill = null;
        foreach(var zomb in leftSpawner.zombies)
        {
            if (zomb.canBeKilled && zomb.isLeft)
            {
                zombToKill = zomb;
                break;
            }
        }

        if(zombToKill != null) { 
            leftSpawner.KillZombie(zombToKill);
            if (!playerSound.isPlaying)
            {
                playerSound.clip = playerData.joyClips[Random.Range(0, playerData.joyClips.Count - 1)];
                playerSound.Play();
            }
        }
        else
        {
            Debug.LogWarning("No zombie !");
            if (!playerSound.isPlaying)
            {
                playerSound.clip = playerData.frustrationClips[Random.Range(0, playerData.frustrationClips.Count - 1)];
                playerSound.Play();
            }
        }

        Debug.Log("[" + playerName + "] Play A");
    }
        
    public void OnPlayB()
    {
        if (!_initialized)
            return;

        ZombieInteraction zombToKill = null;
        foreach (var zomb in rightSpawner.zombies)
        {
            if (zomb.canBeKilled && !zomb.isLeft)
            {
                zombToKill = zomb;
                break;
            }
        }
        if (zombToKill != null)
        {
            rightSpawner.KillZombie(zombToKill);
            if (!playerSound.isPlaying)
            {
                playerSound.clip = playerData.joyClips[Random.Range(0, playerData.joyClips.Count - 1)];
                playerSound.Play();
            }
        }
        else
        {
            Debug.LogWarning("No zombie !");
            if (!playerSound.isPlaying)
            {
                playerSound.clip = playerData.frustrationClips[Random.Range(0, playerData.frustrationClips.Count - 1)];
                playerSound.Play();
            }
        }

            Debug.Log("[" + playerName + "] Play B");
    }

    public AnimationCurve tokenCurve;
    public float tokenAnimDuration;

    public void OnMoveLeft()
    {
        if (!_initialized)
            return;

        actualPosIndex--;
        actualPosIndex = Mathf.Clamp(actualPosIndex, 0, 2);

        StopAllCoroutines();
        StartCoroutine(Tools.LerpAlongCurve(token.transform.position, PlayerInfo.instance.tokenAnchors[actualPosIndex].position + Vector3.forward * 2.0f * playerNumber, tokenCurve, tokenAnimDuration, (x) => token.transform.position = x, null, null, true));

        if(!playerSound.isPlaying)
        {
            playerSound.clip = playerData.thinkingClips[Random.Range(0, playerData.thinkingClips.Count - 1)];
            playerSound.Play();
        }
        //token.transform.position = PlayerInfo.instance.tokenAnchors[actualPosIndex].position + Vector3.forward * 2.0f * playerNumber;

        Debug.Log("[" + playerName + "] Move Left");
    }

    public void OnMoveRight()
    {
        if (!_initialized)
            return;

        actualPosIndex++;
        actualPosIndex = Mathf.Clamp(actualPosIndex, 0, 2);
        StopAllCoroutines();
        StartCoroutine(Tools.LerpAlongCurve(token.transform.position, PlayerInfo.instance.tokenAnchors[actualPosIndex].position + Vector3.forward * 2.0f * playerNumber, tokenCurve, tokenAnimDuration, (x) => token.transform.position = x, null, null, true));

        if (!playerSound.isPlaying)
        {
            playerSound.clip = playerData.thinkingClips[Random.Range(0, playerData.thinkingClips.Count - 1)];
            playerSound.Play();
        }

        Debug.Log("[" + playerName + "] Move Right");
    }

    public void OnMoveUp()
    {
        if (!_initialized)
            return;
        Debug.Log("[" + playerName + "] Move Up");
       /* actualPosIndex++;
        actualPosIndex = Mathf.Clamp(actualPosIndex, 0, 2);
        transform.position = PlayerInfo.instance.playerAnchors[actualPosIndex].transform.position;*/
    }

    public void OnMoveDown()
    {
        if (!_initialized)
            return;

        Debug.Log("[" + playerName + "] Move Down");
       /* actualPosIndex--;
        actualPosIndex = Mathf.Clamp(actualPosIndex, 0, 2);
        transform.position = PlayerInfo.instance.playerAnchors[actualPosIndex].transform.position;*/
    }

    public void OnPlayNote()
    {
        if (!_initialized)
            return;

        Debug.Log("[" + playerName + "] played note.");
    }
}
