using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public GraphicUnlockManger graphicUnlockManger;
    public TextAsset inkAsset;
    public Image background;
    public GameObject dialoguePanel;
    public TMP_Text boxName;
    public TMP_Text boxText;
    public Button skipBtn;
    public Button returnBtn;
    public float textSpeed;
    public string currentReadyDialogue;

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

        switch (StaticData.levelIndex)
        {
            case 1:
                graphicUnlockManger.paintPanel.SetActive(true);
                break;
            case 2:
                // this.audioController.PlayBgm(this.audioController.bgm03Clip);
                this.background.sprite = cg1;
                this.currentReadyDialogue = "Level2";
                break;
        }
    }

    private void Update()
    {
        if (hasShownDialogue)
        {
            return;
        }
        if (isInDialogue)
        {
            if (Input.anyKeyDown)
            {
                if (textFinished)
                {
                    GetNextData();
                }
                else
                {
                    if (Input.anyKeyDown)
                    {
                        currentTextSpeed = textSpeed / 10;
                    }
                }
            }
        }
        else if (Input.anyKeyDown && currentReadyDialogue != "")
        {
            StartDialogue(currentReadyDialogue);
        }  
    }

    void GetNextData()
    {
        if (GetData(ref currentName, ref currentWord))
        {
            textFinished = false;
            StartCoroutine(ShowDialogue());
        }
        else
        {
            FinishDialogue();
        }
    }

    void StartDialogue(string path)
    {
        isInDialogue = true;
        dialoguePanel.SetActive(true);
        inkStroy.ChoosePathString(path);
        if (!GetData(ref currentName, ref currentWord))
        {
            Debug.Log("这是一个空结");
        }
        else
        {
            StartCoroutine(ShowDialogue());
        }
    }

    void FinishDialogue()
    {
        if (currentReadyDialogue == "mirror")
        {
            dialoguePanel.SetActive(false);
            hasShownDialogue = true;
            EndScene();
            return;
        }
        isInDialogue = false;
        dialoguePanel.SetActive(false);
        hasShownDialogue = true;
    }

    bool GetData(ref string currentName, ref string currentWord)
    {
        if (!inkStroy.canContinue)
        {
            return false;
        }
        string s = inkStroy.Continue();
        string [] value = s.Split('：');
        currentName = value[0];
        currentWord = value[1];
        if (value.Length == 3)
        {
            this.currentEvent = value[2];
        }
        return true;
    }

    IEnumerator ShowDialogue()
    {
        textFinished = false;
        switch (currentName)
        {
            case "C":
                boxName.text = "长天";
                break;
            case "Q":
                boxName.text = "秋水";
                break;
            default:
                boxName.text = currentName;
                break;
        }
        
        // avatar.sprite = currentSprite;
        boxText.text = "";
        int num = currentWord.Length;
        for(int i = 0; i < num; ++i)
        {
            boxText.text += currentWord[i];

            yield return new WaitForSeconds(currentTextSpeed);
        }
        while (!Input.anyKeyDown)
        {
            yield return null;
        }
        textFinished = true;
        currentTextSpeed = textSpeed;
    }

    void EndScene()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        // this.audioController.bgmFadeOut();
        fade.FadeOut(1f);
        yield return new WaitForSeconds(1f);

        switch (StaticData.levelIndex)
        {
            case 2:
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
                operation.allowSceneActivation = false;
                while (operation.progress < 0.9f)
                {
                    yield return null;
                }

                operation.allowSceneActivation = true;
                fade.FadeIn(1f);
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
                break;
        }

        

        
    }
}
