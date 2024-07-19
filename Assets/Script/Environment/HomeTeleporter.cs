using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTeleporter : MonoBehaviour
{
    public Transform playerDestination;
    public Transform cameraDestination;
    public MissingItemDialogueManager dialogueManager; // Reference to the MissingItemDialogueManager

    private GameObject player;
    private GameObject mainCamera;
    private Anthony_Control playerControl;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerControl = player.GetComponent<Anthony_Control>();
        dialogueManager = FindObjectOfType<MissingItemDialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerControl != null)
        {
            if (playerControl.IsListComplete())
            {
                Debug.Log("Player has all items. Teleporting...");
                if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
                {
                    player.transform.position = playerDestination.transform.position;
                    mainCamera.transform.position = cameraDestination.transform.position;
                }
            }
            else
            {
                Debug.Log("Player does not have all items. Cannot teleport.");
                if (dialogueManager != null)
                {
                    dialogueManager.ShowImageTemporarily(3.0f); // Show image for 3 seconds
                }
            }
        }
    }
}
