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
    private SceneOverlayBehaviour _sceneOverlay;
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
        _sceneOverlay = GameObject.Find("CanvasSceneOverlay")?.GetComponent<SceneOverlayBehaviour>();
    }

    private void Start()
    {
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

        if (_sceneOverlay != null && !_sceneOverlay.isTimeTravelling())
        {
            if (enable)
            {
                _sceneOverlay.SetMenuOverlay();
            }
            else
            {
                _sceneOverlay.gameObject.SetActive(false);
            }
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