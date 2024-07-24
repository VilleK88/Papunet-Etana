using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MadeByCloseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler, ISubmitHandler
{
    Button button;
    public Image buttonImage;
    [HideInInspector] public Sprite originalSprite;
    public Sprite hoverSprite;
    private float originalSpriteWidth;
    private float originalSpriteHeight;
    private float hoverSpriteWidth = 143.4799f;
    private float hoverSpriteHeight = 74.0214f;
    [SerializeField] CursorController cursor;
    [SerializeField] GameObject madeByScreen;
    [SerializeField] GameObject transparentBG;
    [SerializeField] MadeByButton madeByButton;
    void Start()
    {
        button = GetComponent<Button>();
        if (buttonImage != null)
        {
            buttonImage = button.image;
            originalSprite = buttonImage.sprite;
            originalSpriteWidth = buttonImage.rectTransform.rect.width;
            originalSpriteHeight = buttonImage.rectTransform.rect.height;
        }
        cursor.GetComponent<CursorController>();
        if (madeByButton != null)
            madeByButton.GetComponent<MadeByButton>();
    }
    void Update()
    {
        bool input = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Tab);
        if (input)
        {
            SetButton(originalSprite, originalSpriteWidth, originalSpriteHeight);
            madeByScreen.SetActive(false);
            transparentBG.SetActive(false);
            madeByButton.ButtonTextBackToNormal();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        cursor.ChangeCursor(cursor.cursorHover);
        SetButton(hoverSprite, hoverSpriteWidth, hoverSpriteHeight);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SetButton(originalSprite, originalSpriteWidth, originalSpriteHeight);
        madeByScreen.SetActive(false);
        transparentBG.SetActive(false);
        madeByButton.ButtonTextBackToNormal();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        cursor.ChangeCursor(cursor.cursorOriginal);
        SetButton(originalSprite, originalSpriteWidth, originalSpriteHeight);
    }
    public void OnSelect(BaseEventData eventData)
    {
        SetButton(hoverSprite, hoverSpriteWidth, hoverSpriteHeight);
    }
    public void OnSubmit(BaseEventData eventData)
    {
        SetButton(originalSprite, originalSpriteWidth, originalSpriteHeight);
        madeByScreen.SetActive(false);
        transparentBG.SetActive(false);
        madeByButton.ButtonTextBackToNormal();
    }
    public void OnDeselect(BaseEventData eventData)
    {
        SetButton(originalSprite, originalSpriteWidth, originalSpriteHeight);
    }
    public void SetButton(Sprite sprite, float width, float height)
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = sprite;
            RectTransform rectTransform = buttonImage.rectTransform;
            rectTransform.sizeDelta = new Vector2(width, height);
        }
    }
}