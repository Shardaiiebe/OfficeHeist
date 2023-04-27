using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPlayer : MonoBehaviour
{

    private GameObject player;

    private Vector2 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerPosition();

    }

    private void UpdatePlayerPosition()
    {
        playerPosition = player.transform.position;
    }
}
