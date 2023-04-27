using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class MovementControler : MonoBehaviour
{
    [SerializeField]
     private float speed = 5f;
     private LayerMask ComputerLayer;
     private float interactDistance = 2f;
     private string LevelName = "Level2";



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

        Vector3 movement = new(speed * inputX, speed * inputY, 0);
        movement *= Time.deltaTime;

        transform.Translate(movement);
    }

    private void Interact()
    {
        Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, interactDistance, ComputerLayer);

        foreach (Collider2D interactable in interactables)
        {
            if (interactable.CompareTag("Computer"))
            {
                // Check if the computer scene is already loaded
                bool sceneAlreadyLoaded = false;
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.name == LevelName)
                    {
                        sceneAlreadyLoaded = true;
                        break;
                    }
                }

                // Load the computer scene if it isn't already loaded
                if (!sceneAlreadyLoaded)
                {
                    SceneManager.LoadSceneAsync(LevelName, LoadSceneMode.Additive);
                    interactingWithComputer = true;
                }
            }
        }
    }

    private void EndInteraction()
    {
        // Unload all instances of the computer scene
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == LevelName)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }

        interactingWithComputer = false;
    }
}
