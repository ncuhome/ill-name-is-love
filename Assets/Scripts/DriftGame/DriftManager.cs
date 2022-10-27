using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DriftManager : MonoBehaviour
{
    public static DriftManager instance;
    public bool isEnd = false;
    public int mapSceneIndex = 3;
    public int driftScene5Index = 7;
    public int driftScene2Index = 4;

    private FadeManager fade;

    private void Start()
    {
        instance = this;    
        fade = FadeGameObjectManager.instance.fadeCanvas;
    }

    public void EndGame(bool isDie)
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
            case 3:
                AsyncOperation operation = SceneManager.LoadSceneAsync(mapSceneIndex);
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
            case 8:
                AsyncOperation operation2 = SceneManager.LoadSceneAsync(mapSceneIndex);
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
            case 13:
                AsyncOperation operation3 = SceneManager.LoadSceneAsync(driftScene5Index);
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
            case 26:
                AsyncOperation operation4 = SceneManager.LoadSceneAsync(driftScene2Index);
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
            case 32:
                AsyncOperation operation5 = SceneManager.LoadSceneAsync(mapSceneIndex);
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
        }
    }
}
