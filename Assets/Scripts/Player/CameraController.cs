using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    
    public Transform cameraTransform;
    public Transform followTransform;
    
    [SerializeField] float normalMovementSpeed = .1f;
    [SerializeField] float fastMovementSpeed = 1.0f;
    float movementSpeed = 1f;
    [SerializeField] float movementTime = 5f;
    [SerializeField] float rotationAmount = .25f;
    
    public Vector3 newPosition;
    public Quaternion newRotation;

    [Header("Zoom")]
    public bool enableMouseScrollInput = true;
    [SerializeField] float zoomSpeed = 1f;
    [SerializeField] float minZoom = 1f, maxZoom = 50f;
    [SerializeField] AnimationCurve zoomYAxis;
    [SerializeField] AnimationCurve zoomZAxis;
    [SerializeField] public float zoomProgress = .5f;
    [SerializeField] Vector3 zoomPosition;
    [SerializeField] float zoomXRotation;
    [SerializeField] float zoomRotationClose, zoomRotationFar;
    [SerializeField] Quaternion zoomRotation;

    [SerializeField] Vector3 dragStartPosition;
    [SerializeField] Vector3 dragCurrentPosition;
    [SerializeField] Vector3 rotateStartPosition;
    [SerializeField] Vector3 rotateCurrentPosition;

    bool skipFrame = false;
    static bool controllable = false;
    public static void SetControllable(bool value) => controllable = value;
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        newPosition = transform.position;
        newRotation = transform.rotation;

        if (zoomSpeed != 0f)
            zoomSpeed = zoomSpeed / maxZoom;
        zoomPosition = new Vector3(0, zoomYAxis.Evaluate(zoomProgress) * maxZoom, -zoomZAxis.Evaluate(zoomProgress) * maxZoom);
        cameraTransform.localPosition = zoomPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!controllable)
            return;
        else
            cameraTransform.gameObject.SetActive(true);

        if(skipFrame)
        {
            skipFrame = false;
            return;
        }

        if (followTransform)
        {
            transform.position = Vector3.Lerp(transform.position, followTransform.position, Time.deltaTime * fastMovementSpeed * 2f);
        } else
        {
            HandleMovementInput();
            HandleMouseInput();
        }

        bool cancelInput = Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D);

        if (enableMouseScrollInput && Input.mouseScrollDelta.y != 0)
        {
            zoomProgress -= Input.mouseScrollDelta.y * zoomSpeed * 2f;
        } else if (cancelInput)
        {
            followTransform = null;
            newPosition = transform.position;
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
                dragStartPosition = ray.GetPoint(entry);
        }
            



        HandleZoom();
        HandleRotation();
    }

    public void SetFollowTransform(Transform transform)
    {
        skipFrame = true;
        followTransform = transform;
    }

    void HandleMouseInput()
    {
        
        if(Input.GetMouseButtonDown(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
                dragStartPosition = ray.GetPoint(entry);
        }
        if (Input.GetMouseButton(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        
    }

    void HandleMovementInput()
    {
        movementSpeed = Input.GetKey(KeyCode.LeftShift) ?  fastMovementSpeed : normalMovementSpeed;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            newPosition += (transform.forward * movementSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }

        

        if (Input.GetKey(KeyCode.R))
            zoomProgress -= zoomSpeed;

        if (Input.GetKey(KeyCode.F))
            zoomProgress += zoomSpeed;

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }

    void HandleZoom()
    {
        //Zoom
        zoomProgress = Mathf.Clamp(zoomProgress, minZoom / maxZoom, 1);
        zoomPosition = new Vector3(0, zoomYAxis.Evaluate(zoomProgress) * maxZoom, -zoomZAxis.Evaluate(zoomProgress) * maxZoom);

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomPosition, Time.deltaTime * movementTime);

        zoomXRotation = Mathf.Lerp(zoomRotationClose, zoomRotationFar, zoomProgress);
        zoomRotation = Quaternion.Euler(new Vector3(zoomXRotation, cameraTransform.localRotation.y, cameraTransform.localRotation.z));
        cameraTransform.localRotation = Quaternion.Lerp(cameraTransform.localRotation, zoomRotation, Time.deltaTime * movementTime);
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.Q))
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);

        if (Input.GetKey(KeyCode.E))
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);

        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
            rotateStartPosition.z = 0f;
        }

        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;
            rotateCurrentPosition.z = 0f;
            Vector3 difference = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }
}
