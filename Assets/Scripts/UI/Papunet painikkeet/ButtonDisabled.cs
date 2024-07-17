using UnityEngine;
using UnityEngine.UI;
public class ButtonDisabled : MonoBehaviour
{
    private void Awake()
    {
        newButton.interactable = false;
    }
    public Button newButton;
    public void newMethod()
    {
        newButton.interactable = false;
    }
}