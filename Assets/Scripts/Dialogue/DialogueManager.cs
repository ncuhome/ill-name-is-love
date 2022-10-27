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
    public int boxScene1Index = 8;
    public int driftScene2Index = 4;
    public int boxScene4Index = 11;

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
    public bool hasShownDialogue = false;
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
        hasShownDialogue = false;

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
                Debug.Log(currentReadyDialogue);
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
                dialogueCamera.Loaded();
                dialogueTextPanel.SetActive(true);
                dialogueBackground.SetActive(true);
                background.sprite = cg2;
                currentReadyDialogue = "Level18";
                break;
            case 20:
                dialogueCamera.Loaded();
                dialogueTextPanel.SetActive(true);
                dialogueBackground.SetActive(true);
                background.sprite = cg2;
                currentReadyDialogue = "Level20";
                break;
            case 24:
                dialogueCamera.Loaded();
                dialogueTextPanel.SetActive(true);
                dialogueBackground.SetActive(true);
                background.sprite = cg3;
                currentReadyDialogue = "Level24";
                break;
            case 25:
                dialogueCamera.Loaded();
                dialogueTextPanel.SetActive(true);
                dialogueBackground.SetActive(true);
                background.sprite = cg3;
                currentReadyDialogue = "Level25";
                break;
            case 27:
                dialogueCamera.Loaded();
                dialogueTextPanel.SetActive(true);
                dialogueBackground.SetActive(true);
                background.sprite = cg3;
                currentReadyDialogue = "Level27";
                break;
            case 28:
                dialogueCamera.Loaded();
                dialogueTextPanel.SetActive(true);
                dialogueBackground.SetActive(true);
                background.sprite = cg3;
                currentReadyDialogue = "Level28";
                break;
            case 29:
                dialogueCamera.Loaded();
                dialogueTextPanel.SetActive(true);
                dialogueBackground.SetActive(true);
                background.sprite = cg4;
                currentReadyDialogue = "Level29";
                break;
            case 31:
                dialogueCamera.Loaded();
                dialogueTextPanel.SetActive(true);
                dialogueBackground.SetActive(true);
                background.sprite = cg4;
                currentReadyDialogue = "Level31";
                break;
            case 34:
                dialogueCamera.Loaded();
                dialogueTextPanel.SetActive(true);
                dialogueCamera.Loaded();
                currentReadyDialogue = "Level34";
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
        dialogueTextPanel.SetActive(false);
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
            case "Z":
                boxName.text = "老庄";
                break;
            case "X":
                boxName.text = "小庄";
                break;
            case "A":
                boxName.text = "阿孙";
                break;
            case "D":
                boxName.text = "土匪头子";
                break;
            case "甲":
                boxName.text = "土匪甲";
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
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
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
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
                fade.FadeIn(1f);
                dialogueTextPanel.SetActive(false);
                graphicUnlockManger.paintPanel.SetActive(true);
                currentReadyDialogue = "";
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                break;
            case 7:
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
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
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
                fade.FadeIn(1f);
                dialogueTextPanel.SetActive(false);
                graphicUnlockManger.paintPanel.SetActive(true);
                currentReadyDialogue = "";
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                break;
            case 12:
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
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
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
                fade.FadeIn(1f);
                dialogueTextPanel.SetActive(false);
                graphicUnlockManger.paintPanel.SetActive(true);
                currentReadyDialogue = "";
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                break;
            case 16:
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
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
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
                fade.FadeIn(1f);
                dialogueTextPanel.SetActive(false);
                graphicUnlockManger.paintPanel.SetActive(true);
                currentReadyDialogue = "";
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                break;
            case 20:
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
                AsyncOperation operation5 = SceneManager.LoadSceneAsync(boxScene1Index);
                operation5.allowSceneActivation = false;
                while (operation5.progress < 0.9f)
                {
                    yield return null;
                }

                fade.FadeIn(1f);
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                // isFirstFrame = true;
                operation5.allowSceneActivation = true;
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
                break;
            case 24:
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
                AsyncOperation operation6 = SceneManager.LoadSceneAsync(driftScene2Index);
                operation6.allowSceneActivation = false;
                while (operation6.progress < 0.9f)
                {
                    yield return null;
                }

                fade.FadeIn(1f);
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                // isFirstFrame = true;
                operation6.allowSceneActivation = true;
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
                break;
            case 25:
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
                AsyncOperation operation7 = SceneManager.LoadSceneAsync(driftScene4Index);
                operation7.allowSceneActivation = false;
                while (operation7.progress < 0.9f)
                {
                    yield return null;
                }

                fade.FadeIn(1f);
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                // isFirstFrame = true;
                operation7.allowSceneActivation = true;
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
                break;
            case 28:
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
                AsyncOperation operation8 = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                operation8.allowSceneActivation = false;
                while (operation8.progress < 0.9f)
                {
                    yield return null;
                }

                fade.FadeIn(1f);
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                // isFirstFrame = true;
                operation8.allowSceneActivation = true;
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
                break;
            case 29:
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
                fade.FadeIn(1f);
                dialogueTextPanel.SetActive(false);
                graphicUnlockManger.paintPanel.SetActive(true);
                currentReadyDialogue = "";
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                break;
            case 31:
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
                AsyncOperation operation9 = SceneManager.LoadSceneAsync(driftScene3Index);
                operation9.allowSceneActivation = false;
                while (operation9.progress < 0.9f)
                {
                    yield return null;
                }

                fade.FadeIn(1f);
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                // isFirstFrame = true;
                operation9.allowSceneActivation = true;
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
                break;
            case 34:
                currentName = "";
                currentWord = "";
                boxName.text = "";
                boxText.text = "";
                AsyncOperation operation10 = SceneManager.LoadSceneAsync(boxScene4Index);
                operation10.allowSceneActivation = false;
                while (operation10.progress < 0.9f)
                {
                    yield return null;
                }

                fade.FadeIn(1f);
                StaticData.levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", StaticData.levelIndex);
                // isFirstFrame = true;
                operation10.allowSceneActivation = true;
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
                break;
        }
    }
}
