using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Private


    // Public
    // public PlayerInput playerInput;
    public InputActionReference move;
    


    private float walkSpeed = 100f;
    private Vector2 moveDirection;
    private Rigidbody rb;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>();
    }


    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * walkSpeed * Time.deltaTime, 0, moveDirection.y * walkSpeed * Time.deltaTime);
        // rb.AddForce(new Vector3(moveDirection.x * walkSpeed * Time.deltaTime, 0, moveDirection.y * walkSpeed * Time.deltaTime), ForceMode.VelocityChange);
    }
}
