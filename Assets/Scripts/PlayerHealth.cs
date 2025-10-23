using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxLives = 3;
    public int currentLives = 3;

    [Header("Respawn")]
    public Transform respawnPoint; // If null, use start position

    void Start()
    {
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);
        if (respawnPoint == null) respawnPoint = transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.CompareTag("DeadZone"))
        {
            LoseLife();
        }
    }

    public void LoseLife()
    {
        currentLives = Mathf.Max(0, currentLives - 1);
        Debug.Log($"[PlayerHealth] {gameObject.name} lost a life. Remaining: {currentLives}");

        if (currentLives <= 0)
        {
            Debug.Log($"[PlayerHealth] {gameObject.name} is dead. Game Over.");
            // Simple behaviour: deactivate the player
            gameObject.SetActive(false);
        }
        else
        {
            // Respawn at respawnPoint (or start pos)
            transform.position = respawnPoint != null ? respawnPoint.position : Vector3.zero;
            // Optional: reset velocity if Rigidbody present
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
