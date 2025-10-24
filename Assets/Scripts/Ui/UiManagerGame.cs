using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Game
    private bool inGame = false;
    public GameObject healthPanel;
    public TMP_Text player1Name;
    public TMP_Text player2Name;
    private PlayerHealth player1HealthComponent;
    private PlayerHealth player2HealthComponent;
    public TMP_Text player1HealthDisplay;
    public TMP_Text player2HealthDisplay;
    public int player1Health;
    public int player2Health;
    public Button controlsButtonInGame;

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

        // Game

        controlsButtonInGame.onClick.AddListener(() => cd.DisplayControls(healthPanel));

        // Game Over
        restartButton.onClick.AddListener(() => StartGame());



    }

    // Update is called once per frame
    void Update()
    {

        if (inGame == true)
        {
            player1Health = player1HealthComponent.currentLives;
            player2Health = player2HealthComponent.currentLives;
        }
    }

    void StartGame()
    {
        player1Name.text = "Player1";
        player2Name.text = "Player2";
        DisplayHealth();
        healthPanel.SetActive(!healthPanel.activeSelf);

    }

    void DisplayHealth()
    {
        player1HealthDisplay.text = player1Health.ToString();
        player2HealthDisplay.text = player2Health.ToString();

    }



}
