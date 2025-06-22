using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Private


    // Public
    // public PlayerInput playerInput;
    public InputActionReference move;
    public CharacterController characterController;
    public DragController dragController;
    public float walkSpeed = 2f;
    public float turnSmoothTime = 0.1f;

    private Vector2 moveDirection;
    // private Rigidbody rb;
    private float turnSmoothVelocity;



    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>();
    }


    public void FixedUpdate()
    {
        Vector3 direction = new Vector3(moveDirection.x, 0f, moveDirection.y).normalized;

        if (direction.magnitude >= 0.1f) 
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            characterController.Move(direction * walkSpeed * Time.deltaTime);
        }

        // rb.linearVelocity = new Vector3(moveDirection.x * walkSpeed * 100 * Time.deltaTime, rb.linearVelocity.y, moveDirection.y * walkSpeed * 100 * Time.deltaTime);
        // rb.AddForce(new Vector3(moveDirection.x * walkSpeed * Time.deltaTime, 0, moveDirection.y * walkSpeed * Time.deltaTime), ForceMode.VelocityChange);
    }
}
