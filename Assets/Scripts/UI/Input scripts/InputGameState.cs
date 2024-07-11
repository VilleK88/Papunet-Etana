using UnityEngine;
public class InputGameState : InputState
{
    private InputManager manager;
    public InputGameState(InputManager inputManager)
    {
        this.manager = inputManager;
    }
    public void UpdateState()
    {
        bool input = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Tab);
        if (manager.isEndingMenuOpen)
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