using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, IBehave
{
    [SerializeField]
    private float speed = 1.2f;

    [SerializeField]
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Behave()
    {
        Vector2 playerPos = player.transform.position;

        Vector2 direction = playerPos - (Vector2)transform.position;
        direction.Normalize();

        

        Vector2 newPosition = (Vector2)transform.position + (direction * speed * Time.deltaTime);
        transform.position = newPosition;
        
    }
}
