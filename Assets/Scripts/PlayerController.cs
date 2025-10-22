using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public float speed = 8;
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

    }

}
