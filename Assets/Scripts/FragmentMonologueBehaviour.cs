using UnityEngine;

public class FragmentMonologueBehaviour : DialogueBehaviour
{
    [SerializeField] private int artefactIdx;

    public override DialogueElement[] GiveDialogue()
    {
        if (!GameState.instance.readDescriptions[artefactIdx])
        {
            return null;
        }

        if (GameState.instance.playedGames[artefactIdx])
        {
            return GameManager.instance.GetFragmentPlayAgainText();
        }
        else
        {
            var monologue = GameManager.instance.GetFragmentMonologue();
            GameState.instance.playedGames[artefactIdx] = true;
            return monologue;
        }
    }
}