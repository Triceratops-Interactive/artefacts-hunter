using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [FormerlySerializedAs("_dialogueElements")] [SerializeField]
    private DialogueElement[] introDialogue;

    [SerializeField] private DialogueElement[] afterFirstLevelDialogue;
    [SerializeField] private DialogueElement[] beforeFinalLevelDialogue;
    [SerializeField] private DialogueElement[] afterFinalLevelDialogue;
    [SerializeField] private DialogueElement[] beforeFirstArtefactDescription;
    [SerializeField] private DialogueElement[] afterFirstArtefactDescription;
    [SerializeField] private DialogueElement[] firstFragmentMonologue;
    [SerializeField] private DialogueElement[] furtherFragmentsMonologue;
    [SerializeField] private DialogueElement[] lastFragmentMonologue;
    [SerializeField] private DialogueElement[] fragmentPlayAgainText;
    public DialogueElement[] notReadDescriptionMonologue;

    [SerializeField] private float showFirstReturnDialogueDelay = 2;
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
        if (GameState.instance.NumPlayedGames() == 0)
        {
            return firstFragmentMonologue;
        } else if (GameState.instance.NumPlayedGames() == GameState.NumGames - 1)
        {
            return lastFragmentMonologue;
        }
        else
        {
            return furtherFragmentsMonologue;
        }
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

        if (GameState.instance.shownLastTalk)
        {
            GameObject.Find("Janitor").SetActive(false);
        }
    }

    private void DisplayDialogue()
    {
        if (GameState.instance.NumPlayedGames() == 0 && !GameState.instance.shownIntroDialogue)
        {
            GameState.instance.shownIntroDialogue = true;
            DialogueManager.instance.DisplayDialogue(introDialogue);
        }
        else if (GameState.instance.NumPlayedGames() == 1 && !GameState.instance.shownFirstReturn)
        {
            GameState.instance.shownFirstReturn = true;
            var playerAnim = GameObject.Find("Player").GetComponent<Animator>();
            playerAnim.Play("PlayerBreakdown");
            Invoke(nameof(ShowDialogueAfterFirstMinigame), showFirstReturnDialogueDelay);
        }
        else if (GameState.NumGames - GameState.instance.NumPlayedGames() == 1 &&
                 !GameState.instance.shownLastFightTalk)
        {
            GameState.instance.shownLastFightTalk = true;
            DialogueManager.instance.DisplayDialogue(beforeFinalLevelDialogue);
        } else if (GameState.instance.NumPlayedGames() == GameState.NumGames && !GameState.instance.shownLastTalk)
        {
            GameState.instance.shownLastTalk = true;
            DialogueManager.instance.DisplayDialogue(afterFinalLevelDialogue, FadeOutJanitor);
        }
    }

    private void ShowDialogueAfterFirstMinigame()
    {
        GameObject.Find("Player").GetComponent<Animator>().Play("PlayerIdleUp");
        DialogueManager.instance.DisplayDialogue(afterFirstLevelDialogue);
    }

    private void FadeOutJanitor()
    {
        GameObject.Find("Janitor").GetComponent<JanitorBehaviour>().FadeOut();
    }
}