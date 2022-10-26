using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkAsset;
    public SpriteRenderer background;
    public GameObject dialoguePanel;
    public TMP_Text boxName;
    public TMP_Text boxText;
    public Button skipBtn;
    public Button returnBtn;
    public float textSpeed;

    public Sprite cg1;
    public Sprite cg2;
    public Sprite cg3;
    public Sprite cg4;
    public Sprite cg5;
    public Sprite cg6;
    public Sprite cg7;
    public Sprite cg8;
    public Sprite cg9;

    private float typingTimer;
    private FadeManager fade;
    private Story inkStroy;

    private bool isInDialogue = false;
    private bool textFinished = false;
    private bool hasShownDialogue = false;
    private bool isAnyKeyDown = false;

    private string currentName;
    private string currentWord;
    private string currentEvent;
    private string currentReadyDialogue;
    private float currentTextSpeed;

    private void Start()
    {
        skipBtn.interactable = true;
        returnBtn.interactable = true;
        
        // this.audioController = AudioController.instance;
        fade = FadeGameObjectManager.instance.fadeCanvas;
        dialoguePanel.SetActive(false);
        currentTextSpeed = textSpeed;
        inkStroy = new Story(inkAsset.text);
        
    }
}
