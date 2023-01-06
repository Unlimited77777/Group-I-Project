using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ViewController : MonoBehaviour
{

    // public float speed = 1;
    // public float mouseSpeed = 2000;
    // // Update is called once per frame
    // void Update()
    // {
    //     float h = Input.GetAxis("Horizontal");
    //     float v = Input.GetAxis("Vertical");
    //     float mouse = Input.GetAxis("Mouse ScrollWheel");
    //     Debug.Log(mouse);
    //     transform.Translate(new Vector3(h * speed, mouse * mouseSpeed, v * speed) * Time.deltaTime, Space.World);
        
    // }

    private CameraControls cameraActions;
    private InputAction movement;
    private Transform cameraTransform;

    //Horizontal Translation
    public float maxSpeed = 5f;
    private float speed;
    public float acceleration = 10f;
    public float damping = 15f;
    
    //Vertical Translation
    public float stepSize = 2f;
    public float zoomDampening = 7.5f;
    public float minHeight = 5f;
    public float maxHeight = 50f;
    public float zoomSpeed = 2f;

    //Rotation
    public float maxRotationSpeed = 1f;

    //Edge Movement
    [SerializeField]
    [Range(0f,0.1f)]
    private float edgeTolerance = 0.05f;

    //value set in various functions
    //used to update the position of the camera base object.
    private Vector3 targetPosition;


    //used to track and maintain velocity w/o a rigidbody
    private Vector3 horizontalVelocity;
    private Vector3 lastPosition;

    //tracks where the dragging action started
    Vector3 startDrag;

    void Update()
    {
        //inputs
        GetKeyboardMovement();
        CheckMouseAtScreenEdge();

        //move base and camera objects
        UpdateVelocity();
        UpdateBasePosition();
        UpdateCameraPosition();
    }

    private void Awake()
    {
        cameraActions = new CameraControls();
        cameraTransform = this.GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable()
    {
        cameraTransform.LookAt(this.transform);


        lastPosition = this.transform.position;

        movement = cameraActions.Camera.MoveCamera;
        cameraActions.Camera.RotateCamera.performed += RotateCamera;
        cameraActions.Camera.Enable();
    }

    private void OnDisable()
    {
        cameraActions.Camera.RotateCamera.performed -= RotateCamera;
        cameraActions.Camera.Disable();
    }

    //gets the horizontal forward vector of the camera
    private Vector3 GetCameraForward()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        return forward;
    }

    //gets the horizontal right vector of the camera
    private Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0f;
        return right;
    }

    private void UpdateVelocity()
    {
        horizontalVelocity = (this.transform.position - lastPosition) / Time.deltaTime;
        horizontalVelocity.y = 0f;
        lastPosition = this.transform.position;
    }

    private void GetKeyboardMovement()
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight() 
            + movement.ReadValue<Vector2>().y * GetCameraForward();

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f)
        {
            targetPosition += inputValue;
        }
    }

    private void UpdateBasePosition()
    {
        if (targetPosition.sqrMagnitude > 0.1f)
        {
            //create a ramp up or acceleration
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
            transform.position += targetPosition * speed * Time.deltaTime;
        }
        else
        {
            //create smooth slow down
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
        }

        //reset for next frame
        targetPosition = Vector3.zero;
    }

    //rotate camera
    private void RotateCamera(InputAction.CallbackContext obj)
    {
        if (!Mouse.current.middleButton.isPressed)
        {
            return;
        }
        
        float inputValue = obj.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(cameraTransform.localPosition.x, inputValue * maxRotationSpeed + transform.rotation.eulerAngles.y, 0f);
    }

    

    private void UpdateCameraPosition()
    {
        cameraTransform.LookAt(this.transform);
    }

    private void CheckMouseAtScreenEdge()
    {
        //mouse position is in pixels
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 moveDirection = Vector3.zero;

        //horizontal scrolling
        if(mousePosition.x < edgeTolerance * Screen.width)
        {
            moveDirection += -GetCameraRight();
        }
        else if (mousePosition.x > (1f - edgeTolerance) * Screen.width)
        {
            moveDirection += GetCameraRight();
        }

        //vertical scrolling
        if (mousePosition.y < edgeTolerance * Screen.height)
        {
            moveDirection += -GetCameraForward();
        }
        else if ( mousePosition.y > (1f - edgeTolerance) * Screen.height)
        {
            moveDirection += GetCameraForward();
        }

        targetPosition += moveDirection;
    }
}
