using UnityEngine;
using UnityEngine.UI;

public class ListenButton : MonoBehaviour
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

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.image;
        soundOnOriginalSprite = buttonImage.sprite;
        soundOnSpeechBubble.gameObject.SetActive(false);
    }

    public void Update()
    {
        isMutedFetch = soundManager.GetComponent<SoundManager>().isMuted;

        if(!isMutedFetch)
        {
            soundOn = true;
        }
        else
        {
            soundOn = false;
        }

        if(soundOn)
        {
            soundOffSpeechBubble.gameObject.SetActive(false);

            if (RectTransformUtility.RectangleContainsScreenPoint(buttonImage.rectTransform,
    Input.mousePosition))
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
            if (RectTransformUtility.RectangleContainsScreenPoint(buttonImage.rectTransform,
    Input.mousePosition))
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

        if(!soundOn)
        {
            soundOnSpeechBubble.gameObject.SetActive(false);
        }
    }
}
