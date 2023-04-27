using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField]
    public GameObject waypointWrapper;

    private List<Transform> waypoints = new List<Transform>();
    //private agentmesh
    private int currentTarget;
    private bool targetReached;
    private Vector2 destination;
    private Vector2 currentLocation;
    private float speed;
    private bool isWaiting = false;


    private bool reverse;

    void Start()
    {
        destination = new Vector2(0, 0);
        currentTarget = 0;
        currentLocation = transform.position;
        targetReached = false;
        

        foreach (Component component in waypointWrapper.GetComponentsInChildren<Transform>())
        {

            waypoints.Add((Transform)component);
        }

        //meest belachelijke oplossing maar het werkt 
        waypoints.RemoveAt(0);
        speed = waypoints[currentTarget].GetComponent<WaypointParameter>().movingspeed;
    }

    void Update()
    {
        if (currentTarget < 0) currentTarget = 0;
        destination = waypoints[currentTarget].position;
        float distance = Vector2.Distance(transform.position, destination);
        Vector2 direction = destination - currentLocation;
        direction.Normalize();

        currentLocation = transform.position;

        Vector2 newPosition = currentLocation + (direction * speed * Time.deltaTime);
        transform.position = newPosition;

        if (distance < 1f && targetReached == false )
        {
            targetReached = true;
            if (currentTarget == waypoints.Count-1)
            {
                reverse = true;
            }
            else if (currentTarget == 0)
            {
                reverse = false;
            }

            if (reverse == false)
            {
                currentTarget++;
            }
            else
            {
                currentTarget--;
            }

            if (reverse == true)
            {
                if (currentTarget == 0)
                {
                    reverse = false;
                    currentTarget++;
                }
                else
                {
                    currentTarget--;
                }
            }

            if (currentTarget > 0 && currentTarget < waypoints.Count)
            {
                if (reverse) speed = waypoints[currentTarget - 1].GetComponent<WaypointParameter>().movingspeed;
                else if (currentTarget < waypoints.Count - 1) speed = waypoints[currentTarget + 1].GetComponent<WaypointParameter>().movingspeed;
            }

        }
        else if (targetReached == true)
        {
            print(waypoints[currentTarget].name);
            isWaiting = true;
            StartCoroutine(Idle());
        }
    }

    IEnumerator Idle()
    {
        print(waypoints[currentTarget - 1].GetComponent<WaypointParameter>().waittime);
        targetReached = false;
        isWaiting = false;
        //yield return new WaitForSeconds(waypoints[currentTarget-1].GetComponent<WaypointParameter>().waittime);
        yield return new WaitForSeconds(2f);


    }
}
