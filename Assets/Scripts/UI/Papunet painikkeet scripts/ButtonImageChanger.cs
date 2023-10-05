using UnityEngine;
using UnityEngine.UI;

public class ButtonImageChanger : MonoBehaviour
{
    Button button;
    Image buttonImage;
    Sprite originalSprite;
    public Sprite hoverSprite;
    //TextMeshProUGUI buttonText;

    public GameObject speechBubble;

    void Start()
    {
        button = GetComponent<Button>();
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
}
