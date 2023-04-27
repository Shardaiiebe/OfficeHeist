using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementControler : MonoBehaviour
{
    public float speed = 5f;
    public LayerMask interactLayer;
    public float interactDistance = 2f;

    private bool interactingWithComputer = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (interactingWithComputer && Input.GetKeyDown(KeyCode.Escape))
        {
            EndInteraction();
        }

        // Only allow player control if not interacting with computer
        if (!interactingWithComputer)
        {
            HandlePlayerInput();
        }
    }

    private void HandlePlayerInput()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed * inputX, speed * inputY, 0);
        movement *= Time.deltaTime;

        transform.Translate(movement);
    }

    private void Interact()
    {
        Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, interactDistance, interactLayer);

        foreach (Collider2D interactable in interactables)
        {
            if (interactable.CompareTag("Computer"))
            {
                SceneManager.LoadScene(1);
                interactingWithComputer = true;
                break;
            }
        }
    }

    private void EndInteraction()
    {
        SceneManager.LoadScene(0);
        interactingWithComputer = false;
    }
}
