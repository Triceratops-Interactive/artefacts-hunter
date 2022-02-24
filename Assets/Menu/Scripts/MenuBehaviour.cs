using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    private Animator _characterAnimator;
    private GameObject _controlsPanel;
    private GameObject _creditsPanel;

    private void Awake()
    {
        _characterAnimator = GameObject.Find("CharacterImage").GetComponent<Animator>();
        _controlsPanel = GameObject.Find("ControlsPanel");
        _controlsPanel.GetComponent<TextboxBehaviour>().DisableTextbox();
        _creditsPanel = GameObject.Find("CreditsPanel");
        _creditsPanel.GetComponent<TextboxBehaviour>().DisableTextbox();
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowControlsPanel()
    {
        _controlsPanel.SetActive(true);
    }

    public void ShowCreditsPanel()
    {
        _creditsPanel.SetActive(true);
    }
}