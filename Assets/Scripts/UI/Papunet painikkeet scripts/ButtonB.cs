using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonB : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    Button button;
    public GameObject blackBG;
    [SerializeField] InputManager inputManager;
    [SerializeField] CursorController cursor;
    void Start()
    {
        button = GetComponent<Button>();
        if (inputManager != null)
            inputManager.GetComponent<InputManager>();
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