using UnityEngine;

public class FallSound : MonoBehaviour
{
    public AudioSource audioChute;
    public float seuilY; // Position Y en dessous de laquelle on considère qu’il est tombé

    void Update()
    {
        if (transform.position.y < seuilY && !audioChute.isPlaying)
        {
            audioChute.Play();
            Debug.Log($"FallSound : Le joueur est tombé en dessous de {seuilY} (Position Y: {transform.position.y})");
        }
        else if (transform.position.y >= seuilY && audioChute.isPlaying)
        {
            audioChute.Stop();
            Debug.Log($"FallSound : Le joueur est remonté au-dessus de {seuilY} (Position Y: {transform.position.y})");
        }
    }
}
