using UnityEngine;
using UnityEngine.UI;
public class ButtonDisabled : MonoBehaviour
{
    public Button newButton;
    private void Awake()
    {
        if(newButton != null)
            newButton.interactable = false;
    }
}