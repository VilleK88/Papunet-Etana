using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MadeByCloseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler, ISubmitHandler
{
    Button button;
    public Sprite hoverSprite;
    [SerializeField] CursorController cursor;
    [SerializeField] GameObject madeByScreen;
    [SerializeField] GameObject transparentBG;
    [SerializeField] MadeByButton madeByButton;
    public GameObject hoverImgObj;
    void Start()
    {
        button = GetComponent<Button>();
        cursor.GetComponent<CursorController>();
        if (madeByButton != null)
            madeByButton.GetComponent<MadeByButton>();
    }
    void Update()
    {
        bool input = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Tab);
        if (input)
        {
            hoverImgObj.SetActive(false);
            madeByScreen.SetActive(false);
            transparentBG.SetActive(false);
            madeByButton.ButtonTextBackToNormal();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        cursor.ChangeCursor(cursor.cursorHover);
        hoverImgObj.SetActive(true);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        madeByScreen.SetActive(false);
        transparentBG.SetActive(false);
        madeByButton.ButtonTextBackToNormal();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        cursor.ChangeCursor(cursor.cursorOriginal);
        hoverImgObj.SetActive(false);
    }
    public void OnSelect(BaseEventData eventData)
    {
        hoverImgObj.SetActive(true);
    }
    public void OnSubmit(BaseEventData eventData)
    {
        madeByScreen.SetActive(false);
        transparentBG.SetActive(false);
        madeByButton.ButtonTextBackToNormal();
    }
    public void OnDeselect(BaseEventData eventData)
    {
        hoverImgObj.SetActive(false);
    }
}