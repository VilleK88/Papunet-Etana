using UnityEngine;
public class InputMenuState : InputState
{
    private InputManager manager;
    public InputMenuState(InputManager inputManager)
    {
        this.manager = inputManager;
    }
    public void UpdateState()
    {
        bool input = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Tab);
        if (input)
            manager.NavigateToNextButton();
    }
    public void EnterMenuState()
    {
    }
    public void EnterGameState()
    {
    }
}