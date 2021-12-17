using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [FormerlySerializedAs("_dialogueElements")] [SerializeField]
    private DialogueElement[] introDialogue;

    [SerializeField] private DialogueElement[] beforeFirstArtefactDescription;
    [SerializeField] private DialogueElement[] afterFirstArtefactDescription;
    [SerializeField] private DialogueElement[] firstFragmentMonologue;
    [SerializeField] private DialogueElement[] furtherFragmentsMonologue;
    [SerializeField] private DialogueElement[] fragmentPlayAgainText;

    private Animator _playerAnimator;
    
    private void Awake()
    {
        if (instance != null)
        {
            throw new Exception("tried to instantiate GameManager twice!");
        }

        instance = this;
        _playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
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
        _playerAnimator.runtimeAnimatorController =
            GameState.instance.ingameAnimators[GameState.instance.selectedCharacterIdx];
        if (GameState.instance.NumPlayedGames() == 0)
        {
            DialogueManager.instance.DisplayDialogue(introDialogue);
        }

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
    }
}