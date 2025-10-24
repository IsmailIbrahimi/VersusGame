using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 8;
    public float rotationSpeed = 720;

    public InputActionReference horizontalAction;
    public InputActionReference verticalAction;

    [HideInInspector]
    public float inputMultiplier = 1f;
    public bool isFrozen = false;

    private Rigidbody _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb != null)
        {
            _rb.freezeRotation = true;
        }
    }

    void FixedUpdate()
    {
        if (isFrozen) return;
        
        float horizontal = horizontalAction.action.ReadValue<float>() * inputMultiplier;
        float vertical = verticalAction.action.ReadValue<float>() * inputMultiplier;
        Vector3 moveDir = new Vector3(horizontal, 0, vertical);
        
        _rb.MovePosition(_rb.position + moveDir.normalized * speed * Time.fixedDeltaTime);
        
        if (moveDir != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + 180f;
            Quaternion targetRotation = Quaternion.Euler(90, targetAngle, 0);
            Quaternion newRotation = Quaternion.RotateTowards(_rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            _rb.MoveRotation(newRotation);
        }

        // Verifier si le joueur est en dessous d'une valeure y
        if (this.transform.position.y < 4.5f)
        {
            FindObjectOfType<AudioManager>().Play("Fall");
        }
    }

}
