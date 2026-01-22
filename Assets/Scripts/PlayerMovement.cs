using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction moveAction;
    private Vector3 moveDirection = Vector3.zero;
    public float moveSpeed = 1.0f;

    private void Start()
    {
        moveAction.Enable(); 
        moveAction.performed += GetMoveVector;
        moveAction.canceled += GetMoveVector;
    }

    public void GetMoveVector(InputAction.CallbackContext context)
    {
        var tmp = context.ReadValue<Vector2>();
        moveDirection = new Vector3(tmp.x, 0, tmp.y); 
    }

    private void FixedUpdate()
    {
        transform.position += moveDirection * moveSpeed * Time.fixedDeltaTime;
    }

}
