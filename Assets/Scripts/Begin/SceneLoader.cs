using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    public Button startButtton;
    // public Button continueButton;
    public Button exitButton;
    public GameObject volumeObj;
    public ExitManager exitManager;
    public int defaultSceneIndex = 1;
    public float volumeStep = 0.1f;
    public float fadeDuration = 0.25f;

    ChromaticAberration chromaticAberration;
    private FadeManager fade;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("LevelIndex"))
        {
            StaticData.levelIndex = PlayerPrefs.GetInt("LevelIndex");
            Debug.Log(StaticData.levelIndex);
        }
        else
        {
            PlayerPrefs.SetInt("LevelIndex", 0);
        }
    }

    void Start()
    {
        fade = FadeGameObjectManager.instance.fadeCanvas;
        Volume volume = volumeObj.GetComponent<Volume>();

        if (volume.profile.TryGet<ChromaticAberration>(out chromaticAberration))
        {
            startButtton.onClick.AddListener(() => StartCoroutine(LoadNextScene()));
            // continueButton.onClick.AddListener(() => StartCoroutine("LoadStoreScene"));
            exitButton.onClick.AddListener(() => StartCoroutine("ExitGame"));
        }
    }

    void ExitGame()
    {
        exitManager.gameEnd = true;
    }

    // IEnumerator LoadStoreScene()
    // {
    //     if (StaticData.sceneIndex == 0)
    //     {
    //         StartCoroutine(LoadNextScene());
    //     }
    //     else
    //     {
    //         startButtton.onClick.RemoveAllListeners();
    //         // continueButton.onClick.RemoveAllListeners();
    //         exitButton.onClick.RemoveAllListeners();
    //         while (chromaticAberration.intensity.value < 0.8)
    //         {
    //             chromaticAberration.intensity.value += volumeStep;
    //             yield return new WaitForSeconds(volumeStep);
    //         }

    //         AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + StaticData.sceneIndex);
    //         operation.allowSceneActivation = false;

    //         while (operation.progress < 0.9f)
    //         {
    //             yield return null;
    //         }

    //         operation.allowSceneActivation = true;
    //         // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + StaticData.storeSceneIndex);
    //     }
    // }

    IEnumerator LoadNextScene()
    {
        startButtton.onClick.RemoveAllListeners();
        // continueButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
        while (chromaticAberration.intensity.value < 0.8)
        {
            chromaticAberration.intensity.value += volumeStep;
            yield return new WaitForSeconds(volumeStep);
        }

        fade.FadeOut(fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + defaultSceneIndex);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        operation.allowSceneActivation = true;
        fade.FadeIn(fadeDuration);
        StaticData.levelIndex = 1;
        PlayerPrefs.SetInt("SceneIndex", 1);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
    }
}
