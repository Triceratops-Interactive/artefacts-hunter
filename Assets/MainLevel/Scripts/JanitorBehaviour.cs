using System;
using UnityEngine;

public class JanitorBehaviour : DialogueBehaviour
{
    [SerializeField] private DialogueElement[] beforeFirstMinigameTalk;

    public override (DialogueElement[] dialogue, Action callback) GiveDialogue()
    {
        return (beforeFirstMinigameTalk, null);
    }
}