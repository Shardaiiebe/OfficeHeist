using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public enum BEHAVIOURSTATE { PATROLLING,TRACKING,ATTACKING}   

public class BehaveController : MonoBehaviour
{
    private IBehave currentBehaviour;
    private GameObject player;

    [SerializeField]
    public List<Transform> waypoints = new List<Transform>();

    [SerializeField]
    public GameObject waypointWrapper;

    private BEHAVIOURSTATE behaviourstate = BEHAVIOURSTATE.PATROLLING;

    private List<IBehave> behaviours = new List<IBehave>();
    private float distanceChangeBehaviour = 5f;



    void Start()
    {
        player = GameObject.FindWithTag("Player");

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

       // Debug.Log(behaviours);  
        currentBehaviour = behaviours[1];
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBehaviour();
        ChangeBehaviourClass();
        currentBehaviour.Behave();
        print(behaviourstate);
    }


    private void ChangeBehaviour()
    {
        //daniel feature
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        print(distanceToPlayer);


        if (distanceToPlayer < distanceChangeBehaviour)
        {
            behaviourstate = BEHAVIOURSTATE.ATTACKING;
        }
        else
        {
            behaviourstate = BEHAVIOURSTATE.PATROLLING;
        }
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
