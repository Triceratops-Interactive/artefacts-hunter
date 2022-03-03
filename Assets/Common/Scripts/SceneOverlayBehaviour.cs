using System;
using UnityEngine;
using UnityEngine.UI;

public class SceneOverlayBehaviour : MonoBehaviour
{
    [SerializeField] private Color menuOverlayColor;
    [SerializeField] private Color timeTravelColor;
    [SerializeField] private AudioClip timeTravelClip;
    [SerializeField] private AudioClip reverseTimeTravelClip;
    [SerializeField] private float timeTravelAnimationDuration = 1.594f;
    [SerializeField] private float timeTravelAnimationMinAlpha = 0.1f;
    [SerializeField] private float timeTravelAnimationMaxAlpha = 1;

    private Image _image;
    private float _timeTravelAnimationTime;
    private bool _timeTravelling;
    private bool _reverse;
    private Action _callback;

    public static SceneOverlayBehaviour instance;

    private void Awake()
    {
        if (instance != null)
        {
            throw new Exception("Two overlays!");
        }

        instance = this;
        
        _image = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        if (!_timeTravelling) return;

        _timeTravelAnimationTime -= Time.fixedDeltaTime;

        if (_reverse)
        {
            ReverseTravel();
        }
        else
        {
            ForwardTravel();
        }
    }

    public void SetMenuOverlay()
    {
        _image.color = menuOverlayColor;
        gameObject.SetActive(true);
    }

    public void StartReverseTimeTravel(Action callback)
    {
        _callback = callback;
        _timeTravelAnimationTime = timeTravelAnimationDuration;
        SetTimeTravelColor(timeTravelAnimationMaxAlpha);
        SoundManager.instance.GetEffectSource().PlayOneShot(reverseTimeTravelClip);
        gameObject.SetActive(true);
        _reverse = true;
        _timeTravelling = true;
    }

    public void StartTimeTravel(Action callback)
    {
        _callback = callback;
        _timeTravelAnimationTime = timeTravelAnimationDuration;
        SetTimeTravelColor(timeTravelAnimationMinAlpha);
        SoundManager.instance.GetEffectSource().PlayOneShot(timeTravelClip);
        gameObject.SetActive(true);
        _reverse = false;
        _timeTravelling = true;
    }

    public bool isTimeTravelling()
    {
        return _timeTravelling;
    }

    private void ForwardTravel()
    {
        if (_timeTravelAnimationTime <= 0)
        {
            SetTimeTravelColor(timeTravelAnimationMaxAlpha);
            _timeTravelling = false;
            _callback.Invoke();
            return;
        }

        var timeTravelled = (timeTravelAnimationDuration - _timeTravelAnimationTime) / timeTravelAnimationDuration;
        var alpha = timeTravelAnimationMinAlpha +
                    timeTravelled * (timeTravelAnimationMaxAlpha - timeTravelAnimationMinAlpha);
        SetTimeTravelColor(alpha);
    }

    private void ReverseTravel()
    {
        if (_timeTravelAnimationTime <= 0)
        {
            SetTimeTravelColor(timeTravelAnimationMinAlpha);
            _timeTravelling = false;
            _callback.Invoke();
            return;
        }

        var timeTravelled = _timeTravelAnimationTime / timeTravelAnimationDuration;
        var alpha = timeTravelAnimationMinAlpha +
                    timeTravelled * (timeTravelAnimationMaxAlpha - timeTravelAnimationMinAlpha);
        SetTimeTravelColor(alpha);
    }

    private void SetTimeTravelColor(float alpha)
    {
        _image.color = new Color(timeTravelColor.r, timeTravelColor.g, timeTravelColor.b, alpha);
    }
}