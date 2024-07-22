using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class GuideButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler, ISubmitHandler
{
    Button button;
    [HideInInspector] public Image buttonImage;
    [HideInInspector] public Sprite originalSprite;
    public Sprite hoverSprite;
    public GameObject speechBubble;
    [SerializeField] InputManager inputManager;
    [SerializeField] CursorController cursor;
    [SerializeField] GameObject guideScreen;
    [SerializeField] GameObject transparentBG;
    public bool guideScreenOpen;
    [SerializeField] CloseGuideScreenButton closeGuideScreenButton;
    [SerializeField] GameObject madeByScreen;
    [SerializeField] MadeByButton madeByButton;
    [SerializeField] GuideAudioButton guideAudioButton;
    string currentScene;
    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.image;
        originalSprite = buttonImage.sprite;
        closeGuideScreenButton.GetComponent<CloseGuideScreenButton>();
        speechBubble.gameObject.SetActive(false);
        if (inputManager != null)
            inputManager.GetComponent<InputManager>();
        cursor.GetComponent<CursorController>();
        if (madeByButton != null)
            madeByButton.GetComponent<MadeByButton>();
        if (guideAudioButton != null)
            guideAudioButton.GetComponent<GuideAudioButton>();
        currentScene = SceneManager.GetActiveScene().name;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
            buttonImage.sprite = hoverSprite;
        cursor.ChangeCursor(cursor.cursorHover);
        speechBubble.SetActive(true);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        madeByScreen.SetActive(false);
        if (madeByButton.gameObject.activeSelf)
            madeByButton.ButtonTextBackToNormal();
        inputManager.keyboardInput = false;
        OpenGuideScreen();
        closeGuideScreenButton.blackBG.SetActive(false);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
            buttonImage.sprite = originalSprite;
        cursor.ChangeCursor(cursor.cursorOriginal);
        speechBubble.SetActive(false);
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (buttonImage != null)
            buttonImage.sprite = hoverSprite;
        speechBubble.SetActive(true);
    }
    public void OnSubmit(BaseEventData eventData)
    {
        if (madeByScreen != null)
        {
            madeByScreen.SetActive(false);
            if (madeByButton.gameObject.activeSelf)
                madeByButton.ButtonTextBackToNormal();
        }
        inputManager.keyboardInput = true;
        OpenGuideScreen();
    }
    public void OnDeselect(BaseEventData eventData)
    {
        if (buttonImage != null)
            buttonImage.sprite = originalSprite;
        speechBubble.SetActive(false);
    }
    public void OpenGuideScreen()
    {
        guideScreen.SetActive(true);
        transparentBG.SetActive(true);
        guideScreenOpen = true;
        inputManager.SelectSecondGuideButton();
        if (currentScene == "Game")
            PauseGame();
    }
    public void CloseGuideScreen()
    {
        if (guideAudioButton.buttonImage != null)
            guideAudioButton.buttonImage.sprite = guideAudioButton.originalSprite;
        guideAudioButton.guideAudioOn = false;
        guideScreen.SetActive(false);
        transparentBG.SetActive(false);
        guideScreenOpen = false;
        SoundManager.Instance.source.Stop();
        inputManager.keyboardInput = false;
        if (currentScene == "Game")
            ContinueGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
}