using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager instance;
    public GameObject doorPrefab;
    public GameObject choiceMarkerPrefab;

    public Transform doorHolder;
    public List<Transform> doorAnchors;
    public int nbChoices = 3;

    public float firstDoorDelay = 0.0f;
    public float doorDelay = 5.0f;
    public float voteInitialDelay = 20.0f;
    private float _previousDoorTime = 0.0f;

    public Transform objectToMove;
    private void Awake()
    {
        instance = this;
    }

    public void GenerateDoors()
    {
        var rnd = Random.Range(0, nbChoices);
        for(int i = 0; i < nbChoices; i++)
        {
            if (i == rnd)
                continue;

            var newDoor = Instantiate(doorPrefab);
            newDoor.transform.position = doorAnchors[i].position;
            newDoor.transform.parent = doorHolder;
        }

        var marker = Instantiate(choiceMarkerPrefab);
        marker.transform.parent = doorHolder;
        marker.transform.position = doorAnchors[1].position - Vector3.forward * 2.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        _previousDoorTime = -firstDoorDelay;
    }
    

    public AnimationCurve objectToMoveCurve;
    public float movementDuration = 1.0f;

    public void ComputeChoice()
    {

            var votedIndex = PlayerInfo.instance.GetVoteCountIndex();
            if (votedIndex >= 0)
            {
                var destination = PlayerInfo.instance.tokenAnchors[votedIndex].position - Vector3.forward * 4.0f;
                StartCoroutine(Tools.LerpAlongCurve(objectToMove.position, destination, objectToMoveCurve, movementDuration, (x) => objectToMove.position = x, null, null, true));
            }
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time - _previousDoorTime >= doorDelay)
        {
            _previousDoorTime = Time.time;
            GenerateDoors();
        }
    }
}
