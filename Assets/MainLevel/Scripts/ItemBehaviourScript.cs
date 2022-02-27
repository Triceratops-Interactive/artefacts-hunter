using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ItemBehaviourScript : DialogueBehaviour
{
    [SerializeField] private DialogueElement[] itemFoundDialogue;
    [SerializeField] private String itemToActivate = "";

    private bool boost_enabled = false;

    public override (DialogueElement[] dialogue, Action callback) GiveDialogue()
    {
        return (itemFoundDialogue, activateItem);
    }

    private void activateItem()
    {
        if(itemToActivate == "light sensor")
        {

            ControlLight control_light = GameObject.Find("Player").GetComponentInChildren<ControlLight>();
            control_light.enabled = true;
        }

        if (itemToActivate == "horuseye")
        {
            Camera main_cam = GameObject.Find("Main Camera").GetComponentInChildren<Camera>();
            main_cam.orthographicSize = 10;
            Light2D light = GameObject.Find("Player").GetComponentInChildren<Light2D>();
            light.pointLightOuterRadius = 40;
            light.intensity = 1;
        }

        if(itemToActivate == "boost")
        {
            if(boost_enabled == false)
            {
                boost_enabled = true;
                PlayerBehaviour player_behav = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
                player_behav.speed.x = player_behav.speed.x * 2;
                player_behav.speed.y = player_behav.speed.y * 2;
            }
        }
        Destroy(gameObject);
        
    }



}