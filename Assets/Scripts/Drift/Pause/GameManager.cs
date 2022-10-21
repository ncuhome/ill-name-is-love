using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isEnd = false;
    public GameObject panel;

    private void Start()
    {
        instance = this;    
    }

    public void EndGame(bool isDie)
    {
        isEnd = true;
        Time.timeScale = 0;
        panel.SetActive(true);
    }
}
