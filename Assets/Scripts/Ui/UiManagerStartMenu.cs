using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UiManagerStartMenu : MonoBehaviour
{
    // Start Menu
    public GameObject startMenu;
    public GameObject controlsMenu;
    public GameObject gameChoices;
    public Button startButton, controlsButtonInStart, exitButton;
    public Button arena1, arena2;

    // Controls
    public Button returnToStartButtonInControls;
    public Button returnToStartButtonInArena;
    private ControlsDisplayer cd;

    // Level Loader
    public LevelLoader levelLoader;

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
        arena2.onClick.AddListener(() => loadArena2());

    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame()
    {
        StartCoroutine(StartGameWithTransition());
    }

    IEnumerator StartGameWithTransition()
    {
        // Lancer l'animation de transition
        levelLoader.transition.SetTrigger("Start");

        // Attendre la fin de la transition
        yield return new WaitForSeconds(levelLoader.transitionTime);

        // Changer les menus
        startMenu.SetActive(false);
        gameChoices.SetActive(true);
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void loadArena1()
    {
        StartCoroutine(LoadArenaWithTransition("Game")); // Remplace par le nom exact de ta scène
    }

    void loadArena2()
    {
        StartCoroutine(LoadArenaWithTransition("Game2")); // Remplace par le nom exact de ta scène
    }

    IEnumerator LoadArenaWithTransition(string sceneName)
    {
        // Lancer l'animation de transition
        levelLoader.transition.SetTrigger("Start");

        // Attendre la fin de la transition
        yield return new WaitForSeconds(levelLoader.transitionTime);

        // Charger la scène
        SceneManager.LoadScene(sceneName);
    }

}
