using UnityEngine;
using UnityEngine.InputSystem;

public class DefenseController : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference defenseAction;

    [Header("Defense Settings")]
    public float defenseReduction = 0.15f;

    [Header("Sprites")]
    public Sprite effectSpritePlayer1;
    public Sprite effectSpritePlayer2;

    private bool _isDefending = false;
    private SpriteRenderer playerSprite;
    private Sprite originalSprite;

    public bool IsDefending => _isDefending;

    void OnEnable()
    {
        if (defenseAction != null) defenseAction.action.Enable();
    }

    void OnDisable()
    {
        if (defenseAction != null) defenseAction.action.Disable();
    }

    void Update()
    {
        if (defenseAction != null)
        {
            bool wasDefending = _isDefending;
            _isDefending = defenseAction.action.IsPressed();

            // Change sprite based on defense state
            if (_isDefending && !wasDefending)
            {
                // Started defending
                if (CompareTag("Player1"))
                {
                    playerSprite.sprite = effectSpritePlayer1;
                }
                else if (CompareTag("Player2"))
                {
                    playerSprite.sprite = effectSpritePlayer2;
                }
            }
            else if (!_isDefending && wasDefending)
            {
                // Stopped defending
                playerSprite.sprite = originalSprite;
            }
        }
    }

    // Optional: public method to get effective knockback multiplier
    public float GetKnockbackMultiplier()
    {
        return _isDefending ? defenseReduction : 1f;
    }
}
