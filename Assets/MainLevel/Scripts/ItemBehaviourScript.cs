using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ItemBehaviourScript : DialogueBehaviour
{
    [SerializeField] private DialogueElement[] beforeFirstMinigameTalk;
    [SerializeField] private String itemToActivate = "";

    public override (DialogueElement[] dialogue, Action callback) GiveDialogue()
    {
        return (beforeFirstMinigameTalk, activateItem);
    }

    private void activateItem()
    {
        if(itemToActivate == "light sensor")
        {

            ControlLight control_light = GameObject.Find("Player").GetComponentInChildren<ControlLight>();
            control_light.enabled = true;
        }
    }



}