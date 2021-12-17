using System;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactDescriptionDialogueBehaviour : DialogueBehaviour
{
    [SerializeField] private DialogueElement[] descriptionText;
    [SerializeField] private int artefactIdx;
    
    public override (DialogueElement[] dialogue, Action callback) GiveDialogue()
    {
        GameState.instance.readDescriptions[artefactIdx] = true;
        
        var shownDescription = new List<DialogueElement>(descriptionText.Length);
        if (GameManager.instance.GetDialogueBeforeDescription() != null)
        {
            shownDescription.AddRange(GameManager.instance.GetDialogueBeforeDescription());
        }
        shownDescription.AddRange(descriptionText);
        if (GameManager.instance.GetDialogueBeforeDescription() != null)
        {
            shownDescription.AddRange(GameManager.instance.GetDialogueAfterDescription());
        } 
        
        return (shownDescription.ToArray(), null);
    }
}