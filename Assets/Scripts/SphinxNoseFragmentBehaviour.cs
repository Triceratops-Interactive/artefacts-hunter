using UnityEngine;

public class SphinxNoseFragmentBehaviour : DialogueBehaviour
{
    [SerializeField] private DialogueElement[] fragmentMonologue;
    
    public override DialogueElement[] GiveDialogue()
    {
        return fragmentMonologue;
    }
}