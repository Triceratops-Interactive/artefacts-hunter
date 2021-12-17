using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            throw new Exception("tried to instantiate GameManager twice!");
        }

        instance = this;
    }

    [FormerlySerializedAs("_dialogueElements")] [SerializeField]
    private DialogueElement[] introDialogue;

    [SerializeField] private DialogueElement[] beforeFirstArtefactDescription;
    [SerializeField] private DialogueElement[] afterFirstArtefactDescription;
    [SerializeField] private DialogueElement[] firstFragmentMonologue;
    [SerializeField] private DialogueElement[] furtherFragmentsMonologue;
    [SerializeField] private DialogueElement[] fragmentPlayAgainText;

    public DialogueElement[] GetDialogueBeforeDescription()
    {
        return GameState.instance.NumPlayedGames() == 0 ? beforeFirstArtefactDescription : null;
    }

    public DialogueElement[] GetDialogueAfterDescription()
    {
        return GameState.instance.NumPlayedGames() == 0 ? afterFirstArtefactDescription : null;
    }

    public DialogueElement[] GetFragmentMonologue()
    {
        return GameState.instance.NumPlayedGames() == 0 ? firstFragmentMonologue : furtherFragmentsMonologue;
    }

    public DialogueElement[] GetFragmentPlayAgainText()
    {
        return fragmentPlayAgainText;
    }

    private void Start()
    {
        if (GameState.instance.NumPlayedGames() == 0)
        {
            DialogueManager.instance.DisplayDialogue(introDialogue);
        }
    }
}