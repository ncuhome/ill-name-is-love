using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DriftManager : MonoBehaviour
{
    public static DriftManager instance;
    public bool isEnd = false;
    public int mapSceneIndex = 3;

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
        }
    }
}
