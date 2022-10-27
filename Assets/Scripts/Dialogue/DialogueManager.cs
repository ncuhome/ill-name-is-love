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
    public GameObject dialogueTextPanel;
    public GameObject dialogueBackground;
    public TMP_Text boxName;
    public TMP_Text boxText;
    public Button skipBtn;
    public Button returnBtn;
    public DialogueCamera dialogueCamera;
    public float textSpeed;
    public string currentReadyDialogue;
    public int driftScene3Index = 5;
    public int driftScene4Index = 6;
    public int mapSceneIndex = 3;

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
    // private bool isFirstFrame = true;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;    
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // skipBtn.interactable = true;
        // returnBtn.interactable = true;
        
        // this.audioController = AudioController.instance;
        fade = FadeGameObjectManager.instance.fadeCanvas;
        dialogueBackground.SetActive(false);
        dialogueTextPanel.SetActive(false);
        currentTextSpeed = textSpeed;
        inkStroy = new Story(inkAsset.text);

        Debug.Log("onsceneloaded" + StaticData.levelIndex);

        switch (StaticData.levelIndex)
        {
            case 0:
                break;
            case 1:
                graphicUnlockManger.paintPanel.SetActive(true);
                dialogueBackground.SetActive(true);
                // dialogueTextPanel.SetActive(true);
                dialogueCamera.Loaded();
                break;
            case 5:
                dialogueTextPanel.SetActive(true);
                dialogueCamera.Loaded();
                currentReadyDialogue = "Level5";
                break;
            case 10:
                dialogueTextPanel.SetActive(true);
                dialogueCamera.Loaded();
                currentReadyDialogue = "Level10";
                break;
            case 14:
                dialogueTextPanel.SetActive(true);
                dialogueCamera.Loaded();
                currentReadyDialogue = "Level14";
                break;
            case 18:
                dialogueTextPanel.SetActive(true);
                dialogueBackground.SetActive(true);
                background.sprite = cg2;
                break;
        }
    }

    private void Update()
    {
        // if (isFirstFrame)
        // {
        //     isFirstFrame = false;
        //     this.Start();
        // }
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
        dialogueTextPanel.SetActive(true);
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

    public void FinishDialogue()
    {
        isInDialogue = false;
        hasShownDialogue = true;
        currentReadyDialogue = "";
        EndScene();
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
                AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
                operation.allowSceneActivation = false;
                while (operation.progress < 0.9f)
                {
                    yield return null;
                }

                fade.FadeIn(1f);
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                // isFirstFrame = true;
                operation.allowSceneActivation = true;
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
                break;
            case 5:
                fade.FadeIn(1f);
                dialogueTextPanel.SetActive(false);
                graphicUnlockManger.paintPanel.SetActive(true);
                currentReadyDialogue = "";
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                break;
            case 7:
                AsyncOperation operation2 = SceneManager.LoadSceneAsync(driftScene3Index);
                operation2.allowSceneActivation = false;
                while (operation2.progress < 0.9f)
                {
                    yield return null;
                }

                fade.FadeIn(1f);
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                // isFirstFrame = true;
                operation2.allowSceneActivation = true;
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
                break;
            case 10:
                fade.FadeIn(1f);
                dialogueTextPanel.SetActive(false);
                graphicUnlockManger.paintPanel.SetActive(true);
                currentReadyDialogue = "";
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                break;
            case 12:
                AsyncOperation operation3 = SceneManager.LoadSceneAsync(driftScene4Index);
                operation3.allowSceneActivation = false;
                while (operation3.progress < 0.9f)
                {
                    yield return null;
                }

                fade.FadeIn(1f);
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                // isFirstFrame = true;
                operation3.allowSceneActivation = true;
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
                break;
            case 14:
                fade.FadeIn(1f);
                dialogueTextPanel.SetActive(false);
                graphicUnlockManger.paintPanel.SetActive(true);
                currentReadyDialogue = "";
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                break;
            case 16:
                AsyncOperation operation4 = SceneManager.LoadSceneAsync(mapSceneIndex);
                operation4.allowSceneActivation = false;
                while (operation4.progress < 0.9f)
                {
                    yield return null;
                }

                fade.FadeIn(1f);
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                // isFirstFrame = true;
                operation4.allowSceneActivation = true;
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
                break;
            case 18:
                fade.FadeIn(1f);
                dialogueTextPanel.SetActive(false);
                graphicUnlockManger.paintPanel.SetActive(true);
                currentReadyDialogue = "";
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                break;
        }
    }
}
