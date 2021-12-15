using UnityEngine;

public class SphinxNoseDescriptionBehaviour : DialogueBehaviour
{
    [SerializeField] private DialogueElement[] descriptionText;
    public override DialogueElement[] GiveDialogue()
    {
        return descriptionText;
    }
}