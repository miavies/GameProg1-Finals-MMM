using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookingTime : MonoBehaviour
{
    
    GameObject player;
    Anthony_Control playerControl;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<Anthony_Control>();
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
                    SceneManager.LoadScene("WinScene");
                }
            }
            else
            {
                Debug.Log("Player does not have all items. Cannot teleport.");
            }
        }
    }
}
