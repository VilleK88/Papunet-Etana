using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button button;
    Image buttonImage;
    Sprite originalSprite;
    public Sprite hoverSprite;
    public GameObject speechBubble;

    RectTransform buttonRect;
    Vector2 localMousePosition;
    public bool mouse_over = false;

    void Start()
    {
        button = GetComponent<Button>();
        buttonRect = GetComponent<RectTransform>();
        buttonImage = button.image;
        originalSprite = buttonImage.sprite;
        speechBubble.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (mouse_over)
        {
            Debug.Log("Mouse Over");

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                button.onClick.Invoke();
                buttonImage.sprite = hoverSprite;
                speechBubble.gameObject.SetActive(true);
            }
            else
            {
                buttonImage.sprite = originalSprite;
                speechBubble.gameObject.SetActive(false);
            }
        }


        if (IsMouseOverButton())
        {
            buttonImage.sprite = hoverSprite;
            speechBubble.gameObject.SetActive(true);

        }
        else
        {
            buttonImage.sprite = originalSprite;
            speechBubble.gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
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
