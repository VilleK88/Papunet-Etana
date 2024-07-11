using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class AudioButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    Button button;
    Image buttonImage;
    [SerializeField] Sprite soundOnOriginalSprite;
    [SerializeField] Sprite soundOffOriginalSprite;
    public Sprite soundOnHoverSprite;
    public GameObject soundOnSpeechBubble;
    public Sprite soundOffHoverSprite;
    public GameObject soundOffSpeechBubble;
    bool isMutedFetch;
    bool soundOn = true;
    [SerializeField] InputManager inputManager;
    [SerializeField] CursorController cursor;
    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.image;
        soundOnOriginalSprite = buttonImage.sprite;
        if (SoundManager.Instance.isMuted)
            buttonImage.sprite = soundOffOriginalSprite;
        soundOnSpeechBubble.gameObject.SetActive(false);
        if (inputManager != null)
            inputManager.GetComponent<InputManager>();
        cursor.GetComponent<CursorController>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!SoundManager.Instance.isMuted)
        {
            buttonImage.sprite = soundOnHoverSprite;
            SoundOnSpeechBubble();
        }
        else
        {
            buttonImage.sprite = soundOffHoverSprite;
            SoundOffSpeechBubble();
        }
        cursor.ChangeCursor(cursor.cursorHover);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!SoundManager.Instance.isMuted)
            buttonImage.sprite = soundOnOriginalSprite;
        else
            buttonImage.sprite = soundOffOriginalSprite;
        cursor.ChangeCursor(cursor.cursorOriginal);
        soundOffSpeechBubble.SetActive(false);
        soundOnSpeechBubble.SetActive(false);
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (!SoundManager.Instance.isMuted)
        {
            buttonImage.sprite = soundOnHoverSprite;
            SoundOnSpeechBubble();
        }
        else
        {
            buttonImage.sprite = soundOffHoverSprite;
            SoundOffSpeechBubble();
        }
    }
    public void OnDeselect(BaseEventData eventData)
    {
        if (!SoundManager.Instance.isMuted)
            buttonImage.sprite = soundOnOriginalSprite;
        else
            buttonImage.sprite = soundOffOriginalSprite;
        soundOffSpeechBubble.SetActive(false);
        soundOnSpeechBubble.SetActive(false);
    }
    public void ToggleSoundOnOrOff()
    {
        SoundManager.Instance.isMuted = !SoundManager.Instance.isMuted;
        SoundManager.Instance.source.mute = SoundManager.Instance.isMuted;
        PlayerPrefs.SetInt("isMuted", SoundManager.Instance.isMuted ? 1 : 0);
        PlayerPrefs.Save();
        if (!SoundManager.Instance.isMuted)
        {
            buttonImage.sprite = soundOnHoverSprite;
            SoundOnSpeechBubble();
        }
        else
        {
            buttonImage.sprite = soundOffHoverSprite;
            SoundOffSpeechBubble();
        }
    }
    void SoundOnSpeechBubble()
    {
        soundOffSpeechBubble.SetActive(false);
        soundOnSpeechBubble.SetActive(true);
    }
    void SoundOffSpeechBubble()
    {
        soundOffSpeechBubble.SetActive(true);
        soundOnSpeechBubble.SetActive(false);
    }
}