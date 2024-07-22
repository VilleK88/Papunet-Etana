using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{
    #region Singleton
    public static InputManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        inputMenuState = new InputMenuState(this);
        inputGameState = new InputGameState(this);
        currentInputState = inputMenuState;
    }
    #endregion
    [HideInInspector] public InputState currentInputState;
    [HideInInspector] public InputMenuState inputMenuState;
    [HideInInspector] public InputGameState inputGameState;
    public Button[] buttons;
    public Button[] guideButtons;
    private int currentButtonIndex = 0;
    public bool isEndingMenuOpen;
    [SerializeField] ButtonB playButton;
    [SerializeField] GuideButton guideButton;
    [SerializeField] AudioButton audioButton;
    [SerializeField] ButtonA againButton;
    [SerializeField] ButtonA exitButton;
    [SerializeField] GuideAudioButton guideAudioButton;
    [SerializeField] CloseGuideScreenButton closeGuideScreenButton;
    [SerializeField] ButtonB playAgainButton;
    public bool keyboardInput;
    private void Start()
    {
        if (playButton != null)
            playButton.GetComponent<ButtonB>();
        guideButton.GetComponent<GuideButton>();
        audioButton.GetComponent<AudioButton>();
        againButton.GetComponent<ButtonA>();
        exitButton.GetComponent<ButtonA>();
        guideAudioButton.GetComponent<GuideAudioButton>();
        closeGuideScreenButton.GetComponent<CloseGuideScreenButton>();
        if (playAgainButton != null)
            playAgainButton.GetComponent<ButtonB>();
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainMenu")
        {
            SelectFirstButton();
            currentInputState = inputMenuState;
        }
        else if (sceneName == "Game")
            currentInputState = inputGameState;
    }
    private void Update()
    {
        currentInputState.UpdateState();
    }
    public void SelectFirstButton()
    {
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
    }
    public void SelectSecondGuideButton()
    {
        EventSystem.current.SetSelectedGameObject(guideButtons[1].gameObject);
        keyboardInput = true;
    }
    public void NavigateToNextButton()
    {
        if (!guideButton.guideScreenOpen)
        {
            currentButtonIndex++;
            if (currentButtonIndex >= buttons.Length)
                currentButtonIndex = 0;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
        }
        else
        {
            currentButtonIndex++;
            if (currentButtonIndex >= guideButtons.Length)
                currentButtonIndex = 0;
            EventSystem.current.SetSelectedGameObject(guideButtons[currentButtonIndex].gameObject);
        }
    }
    public void SelectPlayAgainButton()
    {
        playAgainButton.blackBG.SetActive(true);
    }
    public void AgainButtonFunction()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}