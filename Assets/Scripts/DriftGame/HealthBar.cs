using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    public int maxHealth = 3;
    
    private int currentHealth;
    private Image[] images;
    private Color defaultColor;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        images = GetComponentsInChildren<Image>();
        currentHealth = maxHealth;

        defaultColor = images[0].color;
    }

    void Update()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (i < currentHealth)
            {       
                images[i].color = defaultColor;
            }
            else
            {
                images[i].color = Color.white;
            }
        }
    }

    public void DecreaseHeart()
    {
        currentHealth--;
        if (currentHealth == 0 && !DriftManager.instance.isEnd)
        {
            DriftManager.instance.EndGame(false);
        }
    }
}
