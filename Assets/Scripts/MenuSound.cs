using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSound : MonoBehaviour
{
    public Button targetButton;
    public AudioSource audioSource;
    public AudioClip hoverClip;
    public AudioClip clickClip;

    void Start()
    {
        // Ajoute EventTrigger si absent
        EventTrigger trigger = targetButton.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = targetButton.gameObject.AddComponent<EventTrigger>();

        trigger.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();

        // 🎧 Hover (PointerEnter)
        EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
        hoverEntry.eventID = EventTriggerType.PointerEnter;
        hoverEntry.callback.AddListener((eventData) => {
            if (audioSource && hoverClip){
                audioSource.PlayOneShot(hoverClip);
                Debug.Log("Hover sound played");
            }
            else{
                Debug.Log("AudioSource or HoverClip is missing");
            }
        });
        trigger.triggers.Add(hoverEntry);

        // 🖱️ Click (PointerClick)
        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;
        clickEntry.callback.AddListener((eventData) => {
            if (audioSource && clickClip){
                audioSource.PlayOneShot(clickClip);
                Debug.Log("Click sound played");
            }
            else{
                Debug.Log("AudioSource or ClickClip is missing");
            }
        });
        
        trigger.triggers.Add(clickEntry);
    }
}
