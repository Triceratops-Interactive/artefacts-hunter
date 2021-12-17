using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FragmentMonologueBehaviour : DialogueBehaviour
{
    [SerializeField] private int artefactIdx;

    public override (DialogueElement[] dialogue, Action callback) GiveDialogue()
    {
        if (!GameState.instance.readDescriptions[artefactIdx])
        {
            return (null, null);
        }

        if (GameState.instance.playedGames[artefactIdx])
        {
            return (GameManager.instance.GetFragmentPlayAgainText(), LoadMinigame);
        }
        else
        {
            var monologue = GameManager.instance.GetFragmentMonologue();
            return (monologue, LoadMinigame);
        }
    }

    private void LoadMinigame()
    {
        Debug.Log("Loading Game...");
        GameState.instance.playedGames[artefactIdx] = true;
        SceneManager.LoadScene("MainScene");
    }
}