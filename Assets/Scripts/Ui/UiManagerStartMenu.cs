using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManagerStartMenu : MonoBehaviour
{
    // Start Menu
    public GameObject startMenu;
    public GameObject controlsMenu;
    public GameObject gameChoices;
    public Button startButton, controlsButtonInStart, exitButton;
    public Button arena1, arena2, arena3;

    // Controls
    public Button returnToStartButtonInControls;
    public Button returnToStartButtonInArena;
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
        returnToStartButtonInArena.onClick.AddListener(() => cd.ReturnToStartMenu(gameChoices));
        arena1.onClick.AddListener(() => loadArena1());
        arena1.onClick.AddListener(() => loadArena1());
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame()
    {
        //SceneManager.LoadScene("Game");
        startMenu.SetActive(!startMenu.activeSelf);
        gameChoices.SetActive(!gameChoices.activeSelf);
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void loadArena1()
    {

    }
    
    void loadArena2()
    {
        
    }
    
}
