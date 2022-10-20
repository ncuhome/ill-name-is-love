using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image[] hearts;

    private int currentHealth;
    private Image[] sprites;
    // private Color defaultColor;
    private Color hightlightColor = new Color(1f, 0.1075269f, 0f, 1);

    void Start()
    {
        sprites = GetComponentsInChildren<Image>();
        // defaultColor = sprites[0].color;
    }

    void Update()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (i < currentHealth)
            {       
                sprites[i].color = hightlightColor;
            }
            else
            {
                sprites[i].color = Color.white;
            }
        }
    }
}