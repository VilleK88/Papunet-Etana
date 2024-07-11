using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CloseGuideScreenButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    Button button;
    [SerializeField] InputManager inputManager;
    [SerializeField] CursorController cursor;
    public GameObject blackBG;
    void Start()
    {
        button = GetComponent<Button>();
        if (inputManager != null)
        {
            inputManager.GetComponent<InputManager>();
            if (inputManager.keyboardInput)
                blackBG.SetActive(true);
            else
                blackBG.SetActive(false);
        }
        cursor.GetComponent<CursorController>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        blackBG.SetActive(true);
        cursor.ChangeCursor(cursor.cursorHover);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        blackBG.SetActive(false);
        cursor.ChangeCursor(cursor.cursorOriginal);
    }
    public void OnSelect(BaseEventData eventData)
    {
        blackBG.SetActive(true);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        blackBG.SetActive(false);
    }
}