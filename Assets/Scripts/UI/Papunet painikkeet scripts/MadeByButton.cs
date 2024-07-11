using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MadeByButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler, ISubmitHandler
{
    Button button;
    [HideInInspector] public TextMeshProUGUI buttonText;
    [SerializeField] CursorController cursor;
    [SerializeField] GameObject madeByScreen;
    [SerializeField] GameObject transparentBG;
    [SerializeField] GameObject guideScreen;
    [SerializeField] GameObject madeByCloseButtonBlackBG;
    [SerializeField] GuideButton guideButton;
    void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        cursor.GetComponent<CursorController>();
        guideButton.GetComponent<GuideButton>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        cursor.ChangeCursor(cursor.cursorHover);
        buttonText.fontStyle = FontStyles.Bold;
        buttonText.characterSpacing = -4;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        guideButton.CloseGuideScreen();
        madeByScreen.SetActive(true);
        transparentBG.SetActive(true);
        madeByCloseButtonBlackBG.SetActive(false);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        cursor.ChangeCursor(cursor.cursorOriginal);
        if (!madeByScreen.activeSelf)
        {
            ButtonTextBackToNormal();
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        buttonText.fontStyle = FontStyles.Bold;
        buttonText.characterSpacing = -4;
    }
    public void OnSubmit(BaseEventData eventData)
    {
        guideButton.CloseGuideScreen();
        madeByScreen.SetActive(true);
        transparentBG.SetActive(true);
        madeByCloseButtonBlackBG.SetActive(false);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        if (!madeByScreen.activeSelf)
        {
            ButtonTextBackToNormal();
        }
    }
    public void ButtonTextBackToNormal()
    {
        buttonText.fontStyle = FontStyles.Normal;
        buttonText.characterSpacing = 0;
    }
}