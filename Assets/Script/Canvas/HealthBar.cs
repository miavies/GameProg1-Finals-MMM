using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int numOfHearts; // Total number of hearts

    public Image[] hearts; // Array of heart images
    public Sprite fullHeart; // Sprite for full heart

    // Update the UI based on healthBar changes
    public void UpdateHealthBar(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].enabled = true;
                hearts[i].sprite = fullHeart; // Set the sprite to fullHeart
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
