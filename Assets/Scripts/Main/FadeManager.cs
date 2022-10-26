using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FadeManager : MonoBehaviour
{
    public Image img;

    private GameObject volumeObj;
    private Animator fade;
    private DepthOfField depthOfField;
    private ChromaticAberration chromaticAberration;

    void Start()
    {
        volumeObj = GlobalVolumeManager.instance.volumeObj;
        var volume = volumeObj.GetComponent<Volume>();
        volume.profile.TryGet<DepthOfField>(out depthOfField);
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
        fade = GetComponent<Animator>();
    }

    public void FadeOut(float time)
    {
        depthOfField.focusDistance.Override(10f);
        chromaticAberration.intensity.value = 0;
        fade.SetTrigger("FadeOut");
    }

    public void FadeIn(float time)
    {
        fade.SetTrigger("FadeIn");
        // StartCoroutine(Restore(GetClipTime("FadeIn")));
    }
}
