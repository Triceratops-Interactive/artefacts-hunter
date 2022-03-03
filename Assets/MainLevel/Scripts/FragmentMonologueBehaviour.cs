using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FragmentMonologueBehaviour : DialogueBehaviour
{
    [SerializeField] private int artefactIdx;

    private SceneOverlayBehaviour _overlay;

    private void Awake()
    {
        _overlay = GameObject.Find("CanvasSceneOverlay")?.GetComponent<SceneOverlayBehaviour>();
    }

    public override (DialogueElement[] dialogue, Action callback) GiveDialogue()
    {
        if (!GameState.instance.readDescriptions[artefactIdx])
        {
            return (GameManager.instance.notReadDescriptionMonologue, null);
        }

        if (GameState.instance.playedGames[artefactIdx])
        {
            return (GameManager.instance.GetFragmentPlayAgainText(), StartTimetravel);
        }
        else
        {
            var monologue = GameManager.instance.GetFragmentMonologue();
            return (monologue, StartTimetravel);
        }
    }

    private void StartTimetravel()
    {
        _overlay.StartTimeTravel(LoadMinigame);
    }

    private void LoadMinigame()
    {
        Debug.Log("Loading Game...");
        SceneManager.LoadScene(GameManager.instance.minigameScenes[artefactIdx]);
    }
}