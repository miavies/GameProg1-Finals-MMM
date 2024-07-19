using System.Collections;
using UnityEngine;

public class CookingTImeBox : MonoBehaviour
{
    private GameObject player;
    private CookingTimeDialogue dialogueManager; // Reference to the DialogueManager
    private Anthony_Control playerControl;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<Anthony_Control>();
        dialogueManager = FindObjectOfType<CookingTimeDialogue>(); // Find the DialogueManager in the scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerControl != null)
        {
            if (playerControl.IsListComplete())
            {
                dialogueManager.ShowImageTemporarily(3.0f);
            }
            
        }
    }
}