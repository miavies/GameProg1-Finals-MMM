using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform playerDestination;
    public Transform cameraDestination;
    private GameObject player;
    private GameObject mainCamera;
    private RememberListDialogueManager dialogueManager; // Reference to the DialogueManager

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        dialogueManager = FindObjectOfType<RememberListDialogueManager>(); // Find the DialogueManager in the scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Anthony_Control playerControl = player.GetComponent<Anthony_Control>();
            if (playerControl != null)
            {
                if (playerControl.HasList())
                {
                    if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
                    {
                        player.transform.position = playerDestination.position;
                        mainCamera.transform.position = cameraDestination.position;
                    }
                }
                else
                {
                    // Call the method to show the image for 3 seconds if the player does not have the list
                    if (dialogueManager != null)
                    {
                        dialogueManager.ShowImageTemporarily(3f);
                    }
                }
            }
        }
    }
}
