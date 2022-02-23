using System;
using UnityEngine;

public abstract class DialogueBehaviour : MonoBehaviour
{
    public abstract (DialogueElement[] dialogue, Action callback) GiveDialogue();
}