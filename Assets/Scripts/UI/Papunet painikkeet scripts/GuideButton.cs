using UnityEngine;
using UnityEngine.UI;

public class GuideButton : MonoBehaviour
{
    Button button;
    Image buttonImage;
    Sprite originalSprite;
    public Sprite hoverSprite;
    public GameObject speechBubble;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.image;
        originalSprite = buttonImage.sprite;
        speechBubble.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(buttonImage.rectTransform,
            Input.mousePosition))
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
}
