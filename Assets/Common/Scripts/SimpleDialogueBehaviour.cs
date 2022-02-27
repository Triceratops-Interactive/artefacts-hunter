using System;
using UnityEngine;

public class SimpleDialogueBehaviour : DialogueBehaviour
{
    [SerializeField] private DialogueElement[] _dialogue;


    public override (DialogueElement[] dialogue, Action callback) GiveDialogue()
    {
        return (_dialogue, null);
    }
}