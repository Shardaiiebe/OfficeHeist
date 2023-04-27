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
        
        {
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
                if (reverse == false)
                {
                    print("if 1");
                    currentTarget++;
                }
                else
                {
                    currentTarget--;
                    print("if 2");
                }

                if (currentTarget == waypoints.Count)
                {
                    print("if 3");

                    reverse = true;
                }
                else if (currentTarget == 0)
                {
                    print("if 4");

                    reverse = false;
                }

                if (currentTarget != 0)
                {
                    print("if 5");
                    speed = waypoints[currentTarget - 1].GetComponent<WaypointParameter>().movingspeed;

                }
            }
            else if (targetReached == true)
            {
                isWaiting = true;
                StartCoroutine(Idle());
            }
        }
    }

    IEnumerator Idle()
    {
        print(currentTarget);
        print("reverse: " + reverse);
        //print(waypoints[currentTarget - 1].GetComponent<WaypointParameter>().waittime);
        targetReached = false;
        isWaiting = false;
        yield return new WaitForSeconds(waypoints[currentTarget-1].GetComponent<WaypointParameter>().waittime);
         
    }
}
