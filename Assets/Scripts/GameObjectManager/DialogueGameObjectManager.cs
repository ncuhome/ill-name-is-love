using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGameObjectManager : MonoBehaviour
{
    #region Singleton

    public static DialogueGameObjectManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion;

    public DialogueManager dialogueManager;
}
