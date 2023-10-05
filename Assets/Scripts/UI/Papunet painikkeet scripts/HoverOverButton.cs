using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverOverButton : MonoBehaviour
{
    Button button;
    Image buttonImage;
    Sprite originalSprite;
    public Sprite hoverSprite;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.image;
        originalSprite = buttonImage.sprite;
    }

    public void Update()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(buttonImage.rectTransform,
            Input.mousePosition))
        {
            buttonImage.sprite = hoverSprite;

        }
        else
        {
            buttonImage.sprite = originalSprite;
        }
    }
}
