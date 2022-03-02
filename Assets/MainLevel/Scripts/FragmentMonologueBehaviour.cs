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
            return (GameManager.instance.notReadDescriptionMonologue, null);
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
        SceneManager.LoadScene(GameManager.instance.minigameScenes[artefactIdx]);
    }
}