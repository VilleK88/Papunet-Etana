using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button button;
    Image buttonImage;
    [SerializeField] Sprite soundOnOriginalSprite;
    [SerializeField] Sprite soundOffOriginalSprite;
    public Sprite soundOnHoverSprite;
    public GameObject soundOnSpeechBubble;
    public Sprite soundOffHoverSprite;
    public GameObject soundOffSpeechBubble;

    public GameObject soundManager;
    bool isMutedFetch;
    bool soundOn = true;
    public bool dontThrow = false;

    RectTransform buttonRect;
    Vector2 localMousePosition;
    public bool mouse_over = false;

    void Start()
    {
        button = GetComponent<Button>();
        buttonRect = GetComponent<RectTransform>();
        buttonImage = button.image;
        soundOnOriginalSprite = buttonImage.sprite;
        soundOnSpeechBubble.gameObject.SetActive(false);
    }

    public void Update()
    {
        isMutedFetch = soundManager.GetComponent<SoundManager>().isMuted;

        if (!isMutedFetch)
        {
            soundOn = true;
        }
        else
        {
            soundOn = false;
        }


        if (mouse_over)
        {
            Debug.Log("Mouse Over");
            if (soundOn)
            {
                soundOffSpeechBubble.gameObject.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    button.onClick.Invoke();
                    dontThrow = true;
                    buttonImage.sprite = soundOnHoverSprite;
                    soundOnSpeechBubble.gameObject.SetActive(true);
                }
                else
                {
                    dontThrow = false;
                    buttonImage.sprite = soundOnOriginalSprite;
                    soundOnSpeechBubble.gameObject.SetActive(false);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    button.onClick.Invoke();
                    dontThrow = true;
                    buttonImage.sprite = soundOffHoverSprite;
                    soundOffSpeechBubble.gameObject.SetActive(true);
                }
                else
                {
                    dontThrow = false;
                    buttonImage.sprite = soundOffOriginalSprite;
                    soundOffSpeechBubble.gameObject.SetActive(false);
                }
            }

            if (!soundOn)
            {
                soundOnSpeechBubble.gameObject.SetActive(false);
            }
        }


        if (soundOn)
        {
            soundOffSpeechBubble.gameObject.SetActive(false);

            if (IsMouseOverButton())
            {
                dontThrow = true;
                buttonImage.sprite = soundOnHoverSprite;
                soundOnSpeechBubble.gameObject.SetActive(true);

            }
            else
            {
                dontThrow = false;
                buttonImage.sprite = soundOnOriginalSprite;
                soundOnSpeechBubble.gameObject.SetActive(false);
            }
        }
        else
        {
            if (IsMouseOverButton())
            {
                dontThrow = true;
                buttonImage.sprite = soundOffHoverSprite;
                soundOffSpeechBubble.gameObject.SetActive(true);

            }
            else
            {
                dontThrow = false;
                buttonImage.sprite = soundOffOriginalSprite;
                soundOffSpeechBubble.gameObject.SetActive(false);
            }
        }

        if (!soundOn)
        {
            soundOnSpeechBubble.gameObject.SetActive(false);
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
