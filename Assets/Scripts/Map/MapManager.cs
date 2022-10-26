using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public float waitTime = 2f;
    public int driftScene2Index = 4;

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
        }
    }
}
