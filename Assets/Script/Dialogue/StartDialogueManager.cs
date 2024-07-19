using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartDialogueManager : MonoBehaviour
{
    public Image imageToDisable; // Reference to the image you want to disable

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableImageAfterDelay(3f)); // Start the coroutine to disable the image after 3 seconds
    }

    // Coroutine to wait for the delay and then disable the image
    private IEnumerator DisableImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        imageToDisable.gameObject.SetActive(false); // Disable the image
    }
}
