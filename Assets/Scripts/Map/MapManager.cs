using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public float waitTime = 2f;
    public int driftScene2Index = 4;
    public int dialogueSceneIndex = 1;

    private FadeManager fade;

    private void Start()
    {
        fade = FadeGameObjectManager.instance.fadeCanvas;
        StartCoroutine(LoadNextScene(waitTime));    
    }

    IEnumerator LoadNextScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // this.audioController.bgmFadeOut();
        fade.FadeOut(1f);
        yield return new WaitForSeconds(1f);

        switch (StaticData.levelIndex)
        {
            case 4:
                AsyncOperation operation = SceneManager.LoadSceneAsync(driftScene2Index);
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
            case 9:
                AsyncOperation operation2 = SceneManager.LoadSceneAsync(driftScene2Index);
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
            case 17:
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
        }
    }
}
