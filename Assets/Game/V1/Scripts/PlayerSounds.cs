using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerSounds : ScriptableObject
{
    public List<AudioClip> joyClips;
    public List<AudioClip> frustrationClips;
    public List<AudioClip> thinkingClips;

    public Color playerColor;
    public string playerName;
}
