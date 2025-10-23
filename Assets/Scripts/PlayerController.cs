using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 8;
    public float rotationSpeed = 720;

    public InputActionReference horizontalAction;
    public InputActionReference verticalAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = new Vector3(horizontalAction.action.ReadValue<float>(), 0, verticalAction.action.ReadValue<float>());
        this.transform.position += moveDir.normalized * speed * Time.deltaTime;

        if (moveDir != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + 180f;
            Quaternion targetRotation = Quaternion.Euler(90, targetAngle, 0);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
