using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameMenuBehaviour : MonoBehaviour
{
    public static IngameMenuBehaviour instance;

    private Text _pauseText;
    private Button _continueBtn;
    private Button _backToMenuBtn;
    private Button _quitGameBtn;
    private SpriteRenderer _levelSceneOverlay;
    private Image _canvasSceneOverlay;
    private bool _active;

    private void Update()
    {
        var escPressed = Input.GetButtonDown("Cancel");
        if (escPressed)
        {
            EnablePanel(!_active);
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            throw new Exception("Multiple Instances of DialogueManager");
        }

        instance = this;

        _pauseText = GameObject.Find("IngamePauseText").GetComponent<Text>();
        _continueBtn = GameObject.Find("ContinueBtn").GetComponent<Button>();
        _backToMenuBtn = GameObject.Find("BackToMenuBtn").GetComponent<Button>();
        _quitGameBtn = GameObject.Find("QuitGameBtn").GetComponent<Button>();
        _levelSceneOverlay = GameObject.Find("LevelSceneOverlay")?.GetComponent<SpriteRenderer>();
        _canvasSceneOverlay = GameObject.Find("CanvasSceneOverlay")?.GetComponent<Image>();

        EnablePanel(false);
    }

    private void EnablePanel(bool enable)
    {
        _active = enable;
        _continueBtn.gameObject.SetActive(enable);
        _pauseText.gameObject.SetActive(enable);
        _backToMenuBtn.gameObject.SetActive(enable);
        _quitGameBtn.gameObject.SetActive(enable);
        if (_levelSceneOverlay != null)
        {
            _levelSceneOverlay.gameObject.SetActive(enable);
        }

        if (_canvasSceneOverlay != null)
        {
            _canvasSceneOverlay.gameObject.SetActive(enable);
        }
    }

    public bool IsMenuActive()
    {
        return _active;
    }

    public void ContinueBtnPressed()
    {
        EnablePanel(false);
    }

    public void BackToMenuBtnPressed()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGameBtnPressed()
    {
        Application.Quit();
    }
}