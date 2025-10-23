using UnityEngine;

public class HitSound : MonoBehaviour
{
    public AudioSource audioAttaque; // Assigne le son dans l’inspecteur
    public string targetTag = "Player";
    public string enemyTag = "Ennemi";


    void Start()
{
    // Vérifier si nous avons un AudioSource
    if (audioAttaque == null)
    {
        audioAttaque = GetComponent<AudioSource>();
        if (audioAttaque == null)
        {
            Debug.LogWarning("HitSound : Pas d'AudioSource trouvé ! Ajout automatique.");
            audioAttaque = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Debug.Log("HitSound : AudioSource trouvé et assigné.");
        }
    }
}


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) || other.CompareTag(enemyTag))
        {
            audioAttaque.Play();
            Debug.Log($"HitSound : Collision avec {other.name} (Tag: {other.tag})");
        }
    }
}
