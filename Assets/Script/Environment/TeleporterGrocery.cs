using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterGrocery : MonoBehaviour
{

    public Transform playerDestination;
    public Transform cameraDestination;
    GameObject player;
    GameObject groceryCamera;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        groceryCamera = GameObject.FindGameObjectWithTag("GCamera");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
                player.transform.position = playerDestination.transform.position;
                groceryCamera.transform.position = cameraDestination.transform.position;
        }
    }
}
