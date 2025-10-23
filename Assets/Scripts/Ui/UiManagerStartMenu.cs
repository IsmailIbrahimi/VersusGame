using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManagerStartMenu : MonoBehaviour
{
    // Start Menu
    public GameObject startMenu;
    public GameObject controlsMenu;
    public Button startButton, controlsButtonInStart, exitButton;

    // Controls
    public Button returnToStartButtonInControls;
    private ControlsDisplayer cd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cd = FindFirstObjectByType<ControlsDisplayer>();

        // SceneManager.LoadScene("StartMenu");
        print(startMenu.activeSelf);

        // Start menu
        startMenu.SetActive(!startMenu.activeSelf);
        startButton.onClick.AddListener(() => StartGame());
        controlsButtonInStart.onClick.AddListener(() => cd.DisplayControls(startMenu));
        exitButton.onClick.AddListener(() => ExitGame());
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    void ExitGame()
    {
        Application.Quit();
    }
    
}
