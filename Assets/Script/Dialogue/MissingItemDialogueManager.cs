using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MissingItemDialogueManager : MonoBehaviour
{
    public Image imageToDisable; // Reference to the image you want to disable



    // Coroutine to wait for the delay and then disable the image
    private IEnumerator DisableImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        imageToDisable.gameObject.SetActive(false); // Disable the image
    }

    // Method to show the image for a specified duration and then disable it
    public void ShowImageTemporarily(float duration)
    {
        StartCoroutine(ShowAndDisableImageAfterDelay(duration));
    }

    // Coroutine to show the image and then disable it after a delay
    private IEnumerator ShowAndDisableImageAfterDelay(float duration)
    {
        imageToDisable.gameObject.SetActive(true); // Enable the image
        yield return new WaitForSeconds(duration); // Wait for the specified duration
        imageToDisable.gameObject.SetActive(false); // Disable the image
    }
}
