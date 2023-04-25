using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControler : MonoBehaviour
{
    [SerializeField]
    public float speed;


    void Start()
    {

    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(speed * inputX, speed * inputY, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);
    }
}
