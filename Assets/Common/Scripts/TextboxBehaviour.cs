using UnityEngine;

public class TextboxBehaviour : MonoBehaviour
{
    [SerializeField] private bool disableOnAwake = false;

    public static TextboxBehaviour activeTextbox;

    private void Awake()
    {
        if (disableOnAwake) gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        activeTextbox = this;
    }

    public void CloseBtnPressed()
    {
        activeTextbox = null;
        gameObject.SetActive(false);
    }
}