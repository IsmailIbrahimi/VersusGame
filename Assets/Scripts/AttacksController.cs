using UnityEngine;
using UnityEngine.InputSystem;
using SpriteFlash;

public class AttacksController : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference attackAction;

    [Header("Attack Shape")]
    public Transform attackOrigin;
    public float attackRange = 3f;
    public LayerMask targetLayers;
    public string targetTag = "Enemy";

    [Header("Visual Effects")]
    public GameObject slashPrefab;
    public float slashDuration = 0.3f;

    [Header("Knockback")]
    public float knockbackForce = 12f;
    public float knockbackUpward = 1.5f;
    public float knockbackDistance = 3f;

    [Header("Timing")]
    public float attackCooldown = 0.25f;

    private float _lastAttackTime = -999f;

    void OnEnable()
    {
        if (attackAction != null) attackAction.action.Enable();
    }

    void OnDisable()
    {
        if (attackAction != null) attackAction.action.Disable();
    }

    void Update()
    {
        if (attackAction != null && attackAction.action.triggered)
        {
            TryAttack();
        }
    }

    public void TryAttack()
    {
        if (Time.time - _lastAttackTime < attackCooldown) return;
        _lastAttackTime = Time.time;

        Transform oriT = attackOrigin != null ? attackOrigin : transform;
        Vector3 origin = oriT.position;
        Collider[] hits = Physics.OverlapSphere(origin, attackRange, targetLayers, QueryTriggerInteraction.Collide);

        if (slashPrefab != null)
        {
            Vector3 attackDirection = transform.forward;
            SpawnSlashEffect(origin, attackDirection);
        }

        if (hits == null || hits.Length == 0) return;

        foreach (var col in hits)
        {
            if (col == null) continue;
            Transform target = col.attachedRigidbody ? col.attachedRigidbody.transform : col.transform;

            // Filter by tag if specified
            if (!string.IsNullOrEmpty(targetTag) && !target.CompareTag(targetTag)) continue;

            Vector3 toTarget = target.position - origin;
            toTarget.y = 0f;

            ApplyKnockback(target, toTarget.normalized);
        }
    }

    private void ApplyKnockback(Transform targetRoot, Vector3 direction)
    {
        ShieldProtection shield = targetRoot.GetComponent<ShieldProtection>();
        if (shield != null)
        {
            Debug.Log(targetRoot.name + " is protected by shield!");
            return; // L'attaque ne fait rien
        }

        if (direction.sqrMagnitude < 0.0001f) direction = transform.forward;
        direction.Normalize();

        // Check if target is defending
        DefenseController defense = targetRoot.GetComponent<DefenseController>();
        float knockbackMultiplier = (defense != null) ? defense.GetKnockbackMultiplier() : 1f;

        // Trigger flash effect if available
        SimpleFlash flash = targetRoot.GetComponent<SimpleFlash>();
        if (flash != null)
        {
            flash.Flash();
        }

        // Try physics-based knockback first
        Rigidbody rb = targetRoot.GetComponent<Rigidbody>();
        if (rb != null && rb.isKinematic == false)
        {
            Vector3 impulse = (direction * knockbackForce + Vector3.up * knockbackUpward) * knockbackMultiplier;
            rb.AddForce(impulse, ForceMode.Impulse);
            return;
        }

        // Fallback: simple position shove (instant)
        Vector3 shove = direction * knockbackDistance * knockbackMultiplier;
        targetRoot.position += shove;
    }

    private void SpawnSlashEffect(Vector3 origin, Vector3 direction)
    {
        Vector3 slashPosition = origin;
        slashPosition.y = transform.position.y;

        // Spawn the slash
        GameObject slash = Instantiate(slashPrefab, slashPosition, Quaternion.identity);

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            slash.transform.rotation = Quaternion.Euler(90, 0, angle);
        }
        else
        {
            slash.transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        // Destroy after duration
        Destroy(slash, slashDuration);
    }
}
