using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonImageChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button button;
    Image buttonImage;
    Sprite originalSprite;
    public Sprite hoverSprite;
    //TextMeshProUGUI buttonText;

    RectTransform buttonRect;
    Vector2 localMousePosition;
    public bool mouse_over = false;

    public GameObject speechBubble;

    void Start()
    {
        button = GetComponent<Button>();
        buttonRect = GetComponent<RectTransform>();
        buttonImage = button.image;
        originalSprite = buttonImage.sprite;

        //buttonText = GetComponentInChildren<TextMeshProUGUI>();

        //buttonText.gameObject.SetActive(false);

        speechBubble = GetComponent<GameObject>();

        speechBubble.gameObject.SetActive(false);
    }

    public void Update()
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(buttonImage.rectTransform,
            Input.mousePosition))
        {
            buttonImage.sprite = hoverSprite;
            //buttonText.gameObject.SetActive(true);
            speechBubble.gameObject.SetActive(true);

        }
        else
        {
            buttonImage.sprite = originalSprite;
            //buttonText.gameObject.SetActive(false);
            //ohjePuhekupla.gameObject.SetActive(false);
            speechBubble.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        Debug.Log("Mouse exit");
    }

    public bool IsMouseOverButton()
    {
        localMousePosition = buttonRect.InverseTransformPoint(Input.mousePosition);
        return buttonRect.rect.Contains(localMousePosition);
    }
}
