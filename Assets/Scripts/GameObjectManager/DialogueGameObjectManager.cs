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
        if (dialogueManager == null)
        {
            dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        }
    }

    #endregion;

    public DialogueManager dialogueManager;
}
