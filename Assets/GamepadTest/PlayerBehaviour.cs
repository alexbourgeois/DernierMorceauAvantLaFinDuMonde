using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public string playerName;
    private PlayerInputManager _playerInputManager;
    private bool _initialized;

    // Start is called before the first frame update
    void Start()
    {
        _playerInputManager = FindObjectOfType<PlayerInputManager>();
        playerName += _playerInputManager.playerCount;
        _initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayNote()
    {
        if (!_initialized)
            return;

        Debug.Log(playerName + " played note.");
    }
}
