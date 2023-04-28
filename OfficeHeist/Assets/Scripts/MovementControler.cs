using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementControler : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private string[] levelNames;

    private bool interactingWithComputer = false;
    private string currentLevelName = "";

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
                // Check if the computer scene is already loaded
                bool sceneAlreadyLoaded = false;
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.name == currentLevelName)
                    {
                        sceneAlreadyLoaded = true;
                        break;
                    }
                }

                // Load a random level if it isn't already loaded
                if (!sceneAlreadyLoaded)
                {
                    currentLevelName = levelNames[Random.Range(0, levelNames.Length)];
                    SceneManager.LoadSceneAsync(currentLevelName, LoadSceneMode.Additive);
                    interactingWithComputer = true;
                }
            }
        }
    }

    private void EndInteraction()
    {
        // Unload the current level
        SceneManager.UnloadSceneAsync(currentLevelName);
        interactingWithComputer = false;
    }
}
