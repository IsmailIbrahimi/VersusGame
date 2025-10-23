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


    // Update is called once per frame
    void Update()
    {
        if (isFrozen) return;

        float horizontal = horizontalAction.action.ReadValue<float>() * inputMultiplier;
        float vertical = verticalAction.action.ReadValue<float>() * inputMultiplier;
        Vector3 moveDir = new Vector3(horizontal, 0, vertical);
        this.transform.position += moveDir.normalized * speed * Time.deltaTime;
        if (moveDir != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + 180f;
            Quaternion targetRotation = Quaternion.Euler(90, targetAngle, 0);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
