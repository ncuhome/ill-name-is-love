using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    
    public float invincibleDuration = 1f;
    public float hurtShakeDuration = 0.1f;
    public float hurtShakeStrength = 0.05f;
    
    private Boat boat;
    private Image[] hearts;
    private int currentHealth;
    private bool invincible = false;
    private Image[] images;
    private Color defaultColor;

    void Start()
    {
        boat = GetComponent<Boat>();
    }

    public void TakeDamage(int riverIndex, int damege)
    {
        if (!invincible)
        {
            if (damege == 1)
            {
                CameraController.instance.CameraShake(hurtShakeDuration, hurtShakeStrength);
                HealthBar.instance.DecreaseHeart();
            }
            RiverController.instance.MoveAnother(riverIndex);
            StartCoroutine(IntoInvincibility());
            invincible = true;
        }   
    }

    IEnumerator IntoInvincibility()
    {
        float nextInvincibleTime = Time.time + invincibleDuration;
        float startTime = Time.time;
        while (Time.time < nextInvincibleTime)
        {
            //每0.15个无敌周期闪烁一次
            if (((int)((Time.time - startTime) / (0.15f * invincibleDuration))) % 2 == 0)
            {
                boat.SetBoatColorA(0);
            }
            else
            {
                boat.SetBoatColorA(1);
            }
            yield return null;
        }
        boat.SetBoatColorA(1);
        invincible = false;
    }
}