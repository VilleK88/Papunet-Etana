using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonB : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    Button button;
    [HideInInspector] public Image buttonImage;
    [HideInInspector] public Sprite originalSprite;
    public Sprite hoverSprite;
    private float originalSpriteWidth;
    private float originalSpriteHeight;
    public float hoverSpriteWidth;
    public float hoverSpriteHeight;
    [SerializeField] InputManager inputManager;
    [SerializeField] CursorController cursor;
    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.image;
        originalSprite = buttonImage.sprite;
        originalSpriteWidth = buttonImage.rectTransform.rect.width;
        originalSpriteHeight = buttonImage.rectTransform.rect.height;
        if (inputManager != null)
            inputManager.GetComponent<InputManager>();
        cursor.GetComponent<CursorController>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetButton(hoverSprite, hoverSpriteWidth, hoverSpriteHeight);
        cursor.ChangeCursor(cursor.cursorHover);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        SetButton(originalSprite, originalSpriteWidth, originalSpriteHeight);
        cursor.ChangeCursor(cursor.cursorOriginal);
    }
    public void OnSelect(BaseEventData eventData)
    {
        SetButton(hoverSprite, hoverSpriteWidth, hoverSpriteHeight);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        SetButton(originalSprite, originalSpriteWidth, originalSpriteHeight);
    }
    public void SetButton(Sprite sprite, float width, float height)
    {
        if(buttonImage != null)
        {
            buttonImage.sprite = sprite;
            RectTransform rectTransform = buttonImage.rectTransform;
            rectTransform.sizeDelta = new Vector2(width, height);
        }
    }
}