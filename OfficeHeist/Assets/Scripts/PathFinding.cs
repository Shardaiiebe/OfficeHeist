using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour, IBehave
{


    public List<Transform> waypoints;
    public Transform enemyTransform;
    //private agentmesh
    private int currentTarget;
    private bool targetReached;
    private Vector2 destination;
    private Vector2 currentLocation;
    private float speed;
    private bool isWaiting = false;
    private bool reverse;

    public PathFinding(List<Transform> waypoints)
    {
        this.waypoints = waypoints;

        destination = new Vector2(0, 0);
        currentTarget = 0;
        currentLocation = transform.position;
        targetReached = false;




        //meest belachelijke oplossing maar het werkt 
        speed = 2f;

        //speed = waypoints[currentTarget].GetComponent<WaypointParameter>().movingspeed;
    }

    public void Behave()
    {
        destination = waypoints[currentTarget].position;
        float distance = Vector2.Distance(transform.position, destination);

        Vector2 direction = destination - currentLocation;
        direction.Normalize();
        
        // change direction the enemy is looking
        Quaternion targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        float rotationSpeed = 5f;
        enemyTransform.rotation = Quaternion.Lerp(enemyTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
        currentLocation = transform.position;
        Vector2 newPosition = currentLocation + (direction * 2f * Time.deltaTime);
        transform.position = newPosition;
        
        print(isWaiting);
        if (distance < 1f && targetReached == false)
        {
            targetReached = true;


            if (reverse)
            {
                currentTarget--;
            }
            else if (currentTarget < waypoints.Count - 1)
            {
                currentTarget++;
            }

            if (currentTarget == waypoints.Count - 1)
            {

                reverse = true;
            }
            else if (currentTarget == 0)
            {
                reverse = false;
            }

            if (currentTarget != 0)
            {
                speed = waypoints[currentTarget - 1].GetComponent<WaypointParameter>().movingspeed;

            }
        }
        else if (targetReached == true)
        {
            targetReached = false;
        }
    }
}