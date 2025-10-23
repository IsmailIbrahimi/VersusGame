using UnityEngine;

public class FallingSpriteChanger : MonoBehaviour
{
    [Header("Fall Detection")]
    [Tooltip("Y position threshold to trigger falling sprite")]
    public float yThreshold = 5f;

    [Header("Sprites")]
    [Tooltip("Sprite to show when below threshold")]
    public Sprite fallingSprite;

    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private bool isBelowThreshold = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {
        float currentY = transform.position.y;

        // If below threshold and not already changed
        if (currentY < yThreshold && !isBelowThreshold)
        {
            isBelowThreshold = true;
            if (fallingSprite != null)
            {
                spriteRenderer.sprite = fallingSprite;
            }
        }
        // If back above threshold
        else if (currentY >= yThreshold && isBelowThreshold)
        {
            isBelowThreshold = false;
            spriteRenderer.sprite = originalSprite;
        }
    }
}