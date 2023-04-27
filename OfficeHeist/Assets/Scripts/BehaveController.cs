using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public enum BEHAVIOURSTATE { PATROLLING,TRACKING,ATTACKING}   

public class BehaveController : MonoBehaviour
{
    private IBehave currentBehaviour;

    [SerializeField]
    private playerDetection playerDetection;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    public List<Transform> waypoints = new List<Transform>();
    [SerializeField]
    public GameObject waypointWrapper;

    private BEHAVIOURSTATE behaviourstate = BEHAVIOURSTATE.PATROLLING;
    private List<IBehave> behaviours = new List<IBehave>();

    void Start()
    {
        foreach (Component component in waypointWrapper.GetComponentsInChildren<Transform>())
        {
            waypoints.Add((Transform)component);
        }
        waypoints.RemoveAt(0);

        behaviours.Add(GetComponent<Attack>());
        behaviours.Add(GetComponent<PathFinding>());

        PathFinding behaviour = (PathFinding)behaviours[1];
        behaviour.waypoints = waypoints;
        behaviours[1] = behaviour;
        currentBehaviour = behaviours[1];
    }

    void Update()
    {
        ChangeBehaviour();
        ChangeBehaviourClass();
        currentBehaviour.Behave();
    }
    private void ChangeBehaviour() {
        behaviourstate = playerDetection.playerDetected ? BEHAVIOURSTATE.ATTACKING : BEHAVIOURSTATE.PATROLLING;
    }

    private void ChangeBehaviourClass()
    {
        switch(behaviourstate)
        {
            case BEHAVIOURSTATE.ATTACKING:
                currentBehaviour = behaviours[0];
                break;
            case BEHAVIOURSTATE.PATROLLING:
            default:
                currentBehaviour = behaviours[1];
                break;
        }
    }
}
