using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCBehaviour : DialogueBehaviour
{
    [SerializeField] private DialogueElement[] beforeFirstMinigameTalk;

    public override (DialogueElement[] dialogue, Action callback) GiveDialogue()
    {
        return (beforeFirstMinigameTalk, SwitchScene);
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}