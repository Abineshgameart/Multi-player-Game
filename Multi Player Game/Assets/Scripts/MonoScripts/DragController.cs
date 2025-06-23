using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragController : MonoBehaviour
{
    // Public 
    // public Rigidbody rb;
    public InputActionReference look;
    public Transform rightStick;
    public bool isDragging;

    public float forceToAdd = 10f;
    public float turnSmoothTime = 0.1f;

    public Shooter shooterScript;

    public float dragPower = 0f;
    public float dragPowerLimit = 10f;
    public float dragPowerChargeRate = 5f; // How fast the power charges per second

    // Private
    private Vector2 lookDirection;
    private float turnSmoothVelocity;
    [SerializeField] private Projection _projection;
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _ballSpawn;
    private bool wasDraggingPreviously = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = look.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 _direction = new Vector3(lookDirection.x, 0f, lookDirection.y).normalized;

        if (_direction.magnitude >= 0.1f)
        {
            Debug.Log("Drag");
            Drag();
        }
        if (_direction.magnitude < 0.1f)
        {

            if (wasDraggingPreviously)
            {
                Debug.Log("Stick Released - Fire Triggered");
                DragEnd(_direction);
            }
        }

        if (isDragging)
        {
            Debug.Log("Draggging");
            _projection.SimulateTrajectory(_ballPrefab, _ballSpawn.position, _ballSpawn.forward * forceToAdd);
            Dragging(_direction);
        }
        else
        {
            _projection.HideTrajectory();
        }

        // Update Dragging State
        wasDraggingPreviously = (_direction.magnitude >= 0.1f);
    }

    void OnEnable()
    {
        look.action.Enable();
    }

    void OnDisable()
    {
        look.action.Disable();
    }

    void Drag()
    {
        isDragging = true;
    }

    void Dragging(Vector3 l_direction)
    {
        // Just use stick distance from center.
        float distanceMagnitude = l_direction.magnitude; // 0 to 1 range

        // Add distance-based power
        float distancePower = distanceMagnitude * dragPowerChargeRate;

        // Add time-based charging (optional: can be removed if you want pure distance)
        dragPower += (dragPowerChargeRate * Time.fixedDeltaTime) + distancePower;

        // Clamp drag power to limit
        dragPower = Mathf.Min(dragPower, dragPowerLimit);


        float targetAngle = Mathf.Atan2(l_direction.x, l_direction.z) * Mathf.Rad2Deg;
        // Add 180 to face opposite direction
        targetAngle += 180f;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    void DragEnd(Vector3 l_direction)
    {
        isDragging = false;

        // Calculate force based on stick displacement
        float forceAmount = l_direction.magnitude * forceToAdd;

        Fire(dragPower);
        // shooterScript.Fire();

        dragPower = 0f;
    }

    public void Fire(float force)
    {
        var spawned = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);

        spawned.Init(_ballSpawn.forward * force, false);
    }

}
