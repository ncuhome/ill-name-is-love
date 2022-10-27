using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BoxSceneManager : MonoBehaviour
{
    public static BoxSceneManager instance;
    public int boxScene2Index = 9;
    public int boxScene3Index = 10;
    public int dialogueSceneIndex = 1;
    public int boxScene5Index = 12;
    public int boxScene6Index = 13;
    public int box_2SceneIndex = 15;
    public int box_3SceneIndex = 16;

    private FadeManager fade;

    private void Start()
    {
        instance = this;    
        fade = FadeGameObjectManager.instance.fadeCanvas;
    }

    public void EndGame()
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
            case 21:
                AsyncOperation operation = SceneManager.LoadSceneAsync(boxScene2Index);
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
            case 22:
                AsyncOperation operation2 = SceneManager.LoadSceneAsync(boxScene3Index);
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
            case 23:
                AsyncOperation operation3 = SceneManager.LoadSceneAsync(dialogueSceneIndex);
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
            case 35:
                AsyncOperation operation4 = SceneManager.LoadSceneAsync(boxScene5Index);
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
            case 36:
                AsyncOperation operation5 = SceneManager.LoadSceneAsync(boxScene6Index);
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
            case 42:
                AsyncOperation operation6 = SceneManager.LoadSceneAsync(box_2SceneIndex);
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
            case 43:
                AsyncOperation operation7 = SceneManager.LoadSceneAsync(box_3SceneIndex);
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
            case 44:
                AsyncOperation operation8 = SceneManager.LoadSceneAsync(dialogueSceneIndex);
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
        }
    }
}
