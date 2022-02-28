using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [FormerlySerializedAs("_dialogueElements")] [SerializeField]
    private DialogueElement[] introDialogue;

    [SerializeField] private DialogueElement[] beforeFinalLevelDialogue;
    [SerializeField] private DialogueElement[] beforeFirstArtefactDescription;
    [SerializeField] private DialogueElement[] afterFirstArtefactDescription;
    [SerializeField] private DialogueElement[] firstFragmentMonologue;
    [SerializeField] private DialogueElement[] furtherFragmentsMonologue;
    [SerializeField] private DialogueElement[] fragmentPlayAgainText;
    public DialogueElement[] notReadDescriptionMonologue;

    public String[] minigameScenes;

    private void Awake()
    {
        if (instance != null)
        {
            throw new Exception("tried to instantiate GameManager twice!");
        }

        instance = this;
    }
    
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
        SetArtefactVisibility();
        DisplayDialogue();
    }

    private void SetArtefactVisibility()
    {
        if (GameState.instance.playedGames[GameState.SphinxNoseIdx])
        {
            GameObject.Find("SphinxNoseFragment").SetActive(false);
            GameObject.Find("SphinxNoseShowcase").SetActive(false);
        }
        else
        {
            GameObject.Find("SphinxNose").SetActive(false);
        }
        
        if (GameState.instance.playedGames[GameState.DinoBoneIdx])
        {
            GameObject.Find("DinoBoneFragment").SetActive(false);
            GameObject.Find("DinoBoneShowcase").SetActive(false);
        }
        else
        {
            GameObject.Find("DinoBone").SetActive(false);
        }

        if (GameState.NumGames - GameState.instance.NumPlayedGames() > 1)
        {
            GameObject.Find("Laurel").SetActive(false);
            GameObject.Find("LaurelFragment").SetActive(false);
            GameObject.Find("LaurelShowcase").SetActive(false);
            GameObject.Find("LaurelSign").SetActive(false);
        }
        else
        {
            // Played all other games -> Last game should be shown in general
            if (GameState.instance.playedGames[GameState.LaurelIdx])
            {
                GameObject.Find("LaurelFragment").SetActive(false);
            }
            else
            {
                GameObject.Find("Laurel").SetActive(false);
            }   
        }
    }

    private void DisplayDialogue()
    {
        if (GameState.instance.NumPlayedGames() == 0)
        {
            DialogueManager.instance.DisplayDialogue(introDialogue);
        } else if (GameState.NumGames - GameState.instance.NumPlayedGames() == 1)
        {
            DialogueManager.instance.DisplayDialogue(beforeFinalLevelDialogue);
        }
    }
}