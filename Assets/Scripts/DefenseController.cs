using UnityEngine;
using UnityEngine.InputSystem;

public class DefenseController : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference defenseAction;

    [Header("Defense Settings")]
    public float defenseReduction = 0.15f;

    private bool _isDefending = false;

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
            // Check if defense button is held down
            _isDefending = defenseAction.action.IsPressed();
        }
    }

    // Optional: public method to get effective knockback multiplier
    public float GetKnockbackMultiplier()
    {
        return _isDefending ? defenseReduction : 1f;
    }
}
