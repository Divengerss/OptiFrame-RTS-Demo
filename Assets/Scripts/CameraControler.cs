using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;  // Initial movement speed
    public float rotationSpeed = 100f;  // Rotation speed while right-click is held
    public float panSpeed = 10f;  // Speed for planar movement when left-click is held
    public float speedStep = 2f;  // Amount to increase/decrease speed with mouse wheel
    
    private Vector3 pivotPoint;  // Dynamic pivot point for rotation
    private Vector3 moveDirection;
    private bool isRotating = false;
    private bool isPanning = false;
    
    void Update()
    {
        // Handle movement input and speed adjustments
        HandleMovementInput();
        AdjustSpeedWithMouseWheel();

        // Update camera controls based on mouse input
        if (Input.GetMouseButtonDown(1)) isRotating = true;
        if (Input.GetMouseButtonUp(1)) isRotating = false;
        if (Input.GetMouseButtonDown(0)) isPanning = true;
        if (Input.GetMouseButtonUp(0)) isPanning = false;

        // Update the pivot point based on the camera's forward direction
        UpdatePivotPoint();

        // Apply appropriate transformations based on input
        if (isRotating)
        {
            RotateAroundPivot();
        }
        else if (isPanning)
        {
            PanCamera();
        }
        else
        {
            MoveCamera();
        }
    }

    private void HandleMovementInput()
    {
        // Mapping AZQSD and QWASD layouts for movement
        moveDirection = new Vector3(0, 0, 0);
        
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W)) moveDirection += transform.forward;  // Forward
        if (Input.GetKey(KeyCode.S)) moveDirection -= transform.forward;  // Backward
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A)) moveDirection -= transform.right;  // Left
        if (Input.GetKey(KeyCode.D)) moveDirection += transform.right;  // Right
        if (Input.GetKey(KeyCode.Space)) moveDirection += Vector3.up;  // Up
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) moveDirection -= Vector3.up;  // Down
    }

    private void AdjustSpeedWithMouseWheel()
    {
        // Increase/decrease moveSpeed with mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            moveSpeed += scroll * speedStep;
            moveSpeed = Mathf.Max(0f, moveSpeed);  // Ensure speed doesn't go below zero
        }
    }

    private void UpdatePivotPoint()
    {
        // Calculate pivot point on Y = 0 plane where the camera is looking
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            pivotPoint = hit.point;
            pivotPoint.y = 0;  // Keep pivot point at Y = 0 plane
        }
        else
        {
            // Fallback if no object is hit (optional, using forward direction)
            pivotPoint = transform.position + transform.forward * 10f;
            pivotPoint.y = 0;
        }
    }

    private void MoveCamera()
    {
        // Apply the movement based on input, including vertical movement
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }

    private void RotateAroundPivot()
    {
        // Rotate camera around pivot point at Y = 0
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float vertical = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Rotate around pivot for horizontal rotation
        transform.RotateAround(pivotPoint, Vector3.up, horizontal);

        // Pitch rotation (up and down), maintaining the camera's tilt around local X-axis
        transform.RotateAround(pivotPoint, transform.right, vertical);
    }

    private void PanCamera()
    {
        // Move camera on X and Z only when left-click is held
        float horizontal = Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

        // Update position on XZ plane without changing Y
        Vector3 panMovement = new Vector3(horizontal, 0, vertical);
        transform.Translate(panMovement, Space.Self);
    }
}
