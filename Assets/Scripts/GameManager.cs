using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DialogueElement[] _dialogueElements;

    private void Start()
    {
        DialogueManager.instance.DisplayDialogue(_dialogueElements);
    }
}