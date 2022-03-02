using System;
using UnityEngine;

public class DinoLevelBehaviour : MonoBehaviour
{
    [SerializeField] private DialogueElement[] introDialogue;

    private void Start()
    {
        DialogueManager.instance.DisplayDialogue(introDialogue);
    }
}