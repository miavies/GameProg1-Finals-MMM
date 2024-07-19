using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public GameObject notepadCanvas;
    public Image baguetteImage;
    public Image butterImage;
    public Image potatoImage;
    public Image spicesImage;
    public Image wineImage;
    public Image steakImage;
    public Image asparagusImage;

    private void Start()
    {
        // Ensure the canvas starts disabled
        if (notepadCanvas != null)
        {
            notepadCanvas.SetActive(false);
        }

        // Ensure all grocery item images start disabled
        SetAllGroceryImagesActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Anthony_Control playerControl = collision.GetComponent<Anthony_Control>();

            if (playerControl != null)
            {
                if (gameObject.CompareTag("Baguette"))
                {
                    playerControl.PickUpItem("Baguette");
                    SetGroceryImageActive(baguetteImage, true);
                    Destroy(gameObject); // Destroy the item after pickup
                }
                else if (gameObject.CompareTag("List"))
                {
                    playerControl.PickUpItem("List");
                    EnableCanvas();
                    Destroy(gameObject); // Destroy the item after pickup
                }
                else if (gameObject.CompareTag("Butter"))
                {
                    playerControl.PickUpItem("Butter");
                    SetGroceryImageActive(butterImage, true);
                    Destroy(gameObject); // Destroy the item after pickup
                }
                else if (gameObject.CompareTag("Potato"))
                {
                    playerControl.PickUpItem("Potato");
                    SetGroceryImageActive(potatoImage, true);
                    Destroy(gameObject); // Destroy the item after pickup
                }
                else if (gameObject.CompareTag("Spices"))
                {
                    playerControl.PickUpItem("Spices");
                    SetGroceryImageActive(spicesImage, true);
                    Destroy(gameObject); // Destroy the item after pickup
                }
                else if (gameObject.CompareTag("Wine"))
                {
                    playerControl.PickUpItem("Wine");
                    SetGroceryImageActive(wineImage, true);
                    Destroy(gameObject); // Destroy the item after pickup
                }
                else if (gameObject.CompareTag("Steak"))
                {
                    playerControl.PickUpItem("Steak");
                    SetGroceryImageActive(steakImage, true);
                    Destroy(gameObject); // Destroy the item after pickup
                }
                else if (gameObject.CompareTag("Asparagus"))
                {
                    playerControl.PickUpItem("Asparagus");
                    SetGroceryImageActive(asparagusImage, true);
                    Destroy(gameObject); // Destroy the item after pickup
                }
            }
        }
    }

    void EnableCanvas()
    {
        if (notepadCanvas != null)
        {
            notepadCanvas.SetActive(true);
        }
    }

    void SetGroceryImageActive(Image image, bool isActive)
    {
        if (image != null)
        {
            image.gameObject.SetActive(isActive);
        }
    }

    void SetAllGroceryImagesActive(bool isActive)
    {
        SetGroceryImageActive(baguetteImage, isActive);
        SetGroceryImageActive(butterImage, isActive);
        SetGroceryImageActive(potatoImage, isActive);
        SetGroceryImageActive(spicesImage, isActive);
        SetGroceryImageActive(wineImage, isActive);
        SetGroceryImageActive(steakImage, isActive);
        SetGroceryImageActive(asparagusImage, isActive);
    }
}
