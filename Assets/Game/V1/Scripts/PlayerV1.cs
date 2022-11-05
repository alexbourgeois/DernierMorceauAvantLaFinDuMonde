using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        _playerInputManager = FindObjectOfType<PlayerInputManager>();
        playerName = "player " + _playerInputManager.playerCount;
        Debug.Log("Creating " + playerName);

        actualPosIndex = _playerInputManager.playerCount - 1;
        playerNumber = actualPosIndex;
        transform.position = PlayerInfo.instance.playerAnchors[actualPosIndex].transform.position;
        leftSpawner = PlayerInfo.instance.playerAnchors[actualPosIndex].GetComponentsInChildren<SpawnerV1>()[0];
        rightSpawner = PlayerInfo.instance.playerAnchors[actualPosIndex].GetComponentsInChildren<SpawnerV1>()[1];

        playerColor = PlayerInfo.instance.playerColors[actualPosIndex];

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
        foreach (var zomb in leftSpawner.zombies)
        {
            if (zomb.canBeKilled && zomb.isLeft)
            {
                zombToKill = zomb;
            }
        }

        if(zombToKill != null)
            leftSpawner.KillZombie(zombToKill);

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
            }
        }
        if (zombToKill != null)
            rightSpawner.KillZombie(zombToKill);
        else
            Debug.LogWarning("No zombie !");

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
