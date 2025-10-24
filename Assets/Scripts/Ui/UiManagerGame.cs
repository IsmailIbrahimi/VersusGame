using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Game
    public GameObject healthPanel;
    public TMP_Text player1Name;
    public TMP_Text player2Name;
    public TMP_Text player1GameOver;
    public TMP_Text player2GameOver;
    private PlayerHealth player1HealthComponent;
    private PlayerHealth player2HealthComponent;
    public TMP_Text player1HealthDisplay;
    public TMP_Text player2HealthDisplay;
    public Button controlsButtonInGame;

    private bool isFirstTime = true; // for game over

    // Game Visuals
    public GameObject backgroundSky;
    public GameObject arena;
    public GameObject Sumo1;
    public GameObject Sumo2;

    // Game Over
    public GameObject gameOverMenu;
    public Button restartButton;

    private ControlsDisplayer cd;

    private static UiManager _instance;
    virtual protected void Awake()
    {
        if (_instance == null && _instance != this)
        {
            _instance = this as UiManager;
            player1HealthComponent = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerHealth>();
            player2HealthComponent = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerHealth>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static UiManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogWarning("Ui Manager doesn't exist");
            }
            return _instance;
        }
        protected set { }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
        cd = FindFirstObjectByType<ControlsDisplayer>();

        if (AudioManager.instance != null)
        {
            AudioManager.instance.Stop("RoundsTheme"); // Arrêter d'abord au cas où
            AudioManager.instance.Play("RoundsTheme"); // Puis relancer
        }

        // Game
        controlsButtonInGame.onClick.AddListener(() => cd.DisplayControls(healthPanel));

        // Game Over
        restartButton.onClick.AddListener(() => GoToStartMenu());




    }

    // Update is called once per frame
    void Update()
    {

        DisplayHealth();

    }

    void StartGame()
    {
        player1Name.text = "Player1";
        player2Name.text = "Player2";
        healthPanel.SetActive(!healthPanel.activeSelf);

    }

    void GoToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
        FindObjectOfType<AudioManager>().Stop("GameOverMusic");

    }

    void DisplayHealth()
    {
        print($"currentlive of player1: {player1HealthComponent.currentLives}");
        int player1Health = player1HealthComponent.currentLives;
        int player2Health = player2HealthComponent.currentLives;
        player1HealthDisplay.text = player1Health.ToString();
        player2HealthDisplay.text = player2Health.ToString();

        if ((player1Health == 0 || player2Health == 0) && isFirstTime == true)
        {
            healthPanel.SetActive(!healthPanel.activeSelf);
            gameOverMenu.SetActive(!gameOverMenu.activeSelf);

            FindObjectOfType<AudioManager>().Stop("RoundsTheme");
            AudioManager.instance.Play("GameOverMusic");

            if (player1Health == 0)
            {
                player1GameOver.text = $"{player1Name.text}, you lost!";
                player2GameOver.text = $"{player2Name.text}, you won!";
            }
            else
            {
                player1GameOver.text = $"{player1Name.text}, you won!";
                player2GameOver.text = $"{player2Name.text}, you lost!";
            }

            isFirstTime = false;
        }
    }





}
