using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    private Animator _characterAnimator;

    private void Awake()
    {
        _characterAnimator = GameObject.Find("CharacterImage").GetComponent<Animator>();
    }

    public void ChooseLeftCharacter()
    {
        if (--GameState.instance.selectedCharacterIdx < 0)
        {
            GameState.instance.selectedCharacterIdx = GameState.NumCharacters - 1;
        }
        _characterAnimator.runtimeAnimatorController =
            GameState.instance.menuAnimators[GameState.instance.selectedCharacterIdx];
    }

    public void ChooseRightCharacter()
    {
        if (++GameState.instance.selectedCharacterIdx > GameState.NumCharacters - 1)
        {
            GameState.instance.selectedCharacterIdx = 0;
        }
        _characterAnimator.runtimeAnimatorController =
            GameState.instance.menuAnimators[GameState.instance.selectedCharacterIdx];
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}