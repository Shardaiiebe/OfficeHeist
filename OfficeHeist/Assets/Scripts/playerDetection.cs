using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetection : MonoBehaviour
{
    private bool playerDetected = false;
    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player"))
        {
            playerDetected = true;
            print("player seen!");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerDetected = false;
            print("player lost");
        }
    }
}
