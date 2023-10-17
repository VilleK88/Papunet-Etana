using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button button;
    Image buttonImage;
    Sprite originalSprite;
    public Sprite hoverSprite;

    RectTransform buttonRect;
    Vector2 localMousePosition;
    public bool mouse_over = false;

    void Start()
    {
        button = GetComponent<Button>();
        buttonRect = GetComponent<RectTransform>();
        buttonImage = button.image;
        originalSprite = buttonImage.sprite;
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
            }
            else
            {
                buttonImage.sprite = originalSprite;
            }
        }


        if (IsMouseOverButton())
        {
            buttonImage.sprite = hoverSprite;

        }
        else
        {
            buttonImage.sprite = originalSprite;
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
