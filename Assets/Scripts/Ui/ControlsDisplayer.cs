using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;

public class ControlsDisplayer : MonoBehaviour
{
    public GameObject controlsMenu;
    public GameObject healthPanel;
    public GameObject startMenu;
    public Button returnToStartButtonInControls;
    private Rigidbody rb;
    private UiManager mg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void DisplayControls(GameObject activeMenu)
    {
        activeMenu.SetActive(!activeMenu.activeSelf);
        controlsMenu.SetActive(!controlsMenu.activeSelf);

        // if active menu == healthpanel, sleep players 
        print($"active menu: {activeMenu.name}");
        print($"health panel: {healthPanel.name}");
        print($"start menu: {startMenu}");

        returnToStartButtonInControls.onClick.RemoveAllListeners();
        if (activeMenu.name == startMenu.name)
        {
            returnToStartButtonInControls.onClick.AddListener(() => ReturnToStartMenu(controlsMenu));
        }
        else if (activeMenu.name == healthPanel.name)
        {
            returnToStartButtonInControls.onClick.AddListener(() => ReturnToGame(controlsMenu));
            
            mg = FindFirstObjectByType<UiManager>();
            GameObject sumo1 = mg.Sumo1;
            GameObject sumo2 = mg.Sumo2;
            Rigidbody Sumo1RB = sumo1.GetComponent<Rigidbody>();
            Rigidbody Sumo2RB = sumo2.GetComponent<Rigidbody>();

            ToggleSleep(Sumo1RB, Sumo2RB);
        }
    }

    public void ReturnToStartMenu(GameObject activeMenu)
    {
        activeMenu.SetActive(!activeMenu.activeSelf);
        startMenu.SetActive(!startMenu.activeSelf);
    }

    public void ReturnToGame(GameObject activeMenu)
    {
        activeMenu.SetActive(!activeMenu.activeSelf);
        healthPanel.SetActive(!healthPanel.activeSelf);

        mg = FindFirstObjectByType<UiManager>();
        GameObject sumo1 = mg.Sumo1;
        GameObject sumo2 = mg.Sumo2;
        Rigidbody Sumo1RB = sumo1.GetComponent<Rigidbody>();
        Rigidbody Sumo2RB = sumo2.GetComponent<Rigidbody>();

        ToggleSleep(Sumo1RB, Sumo2RB);
    }

    private void ToggleSleep(Rigidbody sumo1, Rigidbody sumo2)
    {
        if (sumo1 != null)
        {
            sumo1.Sleep();
            sumo2.Sleep();
        }
        else
        {
            sumo1.WakeUp();
            sumo2.WakeUp();
        }
    }
}
