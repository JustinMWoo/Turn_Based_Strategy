using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    #region Variables
    // Singleton reference
    public static CameraController instance;
    public Transform panTarget;
    public Vector3 targetFixed;
    public Transform cameraTransform;

    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;

    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    public Vector3 cameraReturnPosition;
    public Quaternion cameraReturnRotation;

    public bool attackCameraActive;
    public bool defocusing;
    public Vector3 battleMidpoint;
    public Vector3 attackPosition;
    public bool doneBattle = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Check mouse is over UI
        if (Time.timeScale != 0)
        {
            if (panTarget != null)
            {
                PanToTarget();
            }
            else if (attackCameraActive)
            {
                FocusCamera();
            }
            else if (defocusing)
            {
                DefocusCamera();
            }
            else
            {
                HandleMouseInput();
                HandleMovementInput();
            }

            if (panTarget != null && Vector3.Distance(transform.position, targetFixed) <= 0.05f)
            {
                panTarget = null;
            }

            // Camera has returned to original position
            if (defocusing && Vector3.Distance(cameraTransform.position, cameraReturnPosition) <= 0.01 && (cameraTransform.rotation == cameraReturnRotation))
            {
                //Debug.Log("Done battle");
                defocusing = false;
                doneBattle = true;
            }
            // Debug.Log("Current camera position: " + cameraTransform.position);
            // Debug.Log("Return position: " + cameraReturnPosition);
        }
    }
    void HandleMouseInput()
    {
        #region Panning
        if (Input.GetMouseButtonDown(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            //Create ray from the location of the mouse click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Sets entry to the distance along the ray where it intersects the plane
            if (plane.Raycast(ray, out float entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        // Update position based on drag distance
        if (Input.GetMouseButton(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
        #endregion

        #region Zoom
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
        #endregion

        #region Rotate
        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
        #endregion
    }

    public void PanToTarget()
    {
        // Remove the y value from the target
        targetFixed = new Vector3(panTarget.position.x, transform.position.y, panTarget.position.z);

        transform.position = Vector3.Lerp(transform.position, targetFixed, Time.deltaTime * movementTime);
        newPosition = transform.position;
    }

    public void FocusOnAttack(Vector3 unit1, Vector3 unit2)
    {
        attackCameraActive = true;
        // Save starting camera position
        cameraReturnPosition = cameraTransform.position;
        cameraReturnRotation = cameraTransform.rotation;

        battleMidpoint = Vector3.Lerp(unit1, unit2, 0.5f);


        // Set camera position to be perpendicular to attack and slightly above
        Vector3 dir = unit1 - unit2;
        // Debug.Log(Vector3.Cross(dir, Vector3.up).normalized);

        // Use distance to set the zoom amount
        float distance = Vector3.Distance(unit1, unit2);

        // Set distance if too low to see health bars
        if (distance < 3)
            distance = 3;

        Vector3 attackPosition1 = Vector3.Cross(dir, Vector3.up).normalized * distance + battleMidpoint;
        Vector3 attackPosition2 = -Vector3.Cross(dir, Vector3.up).normalized * distance + battleMidpoint;

        // Move camera to closer side
        if (Vector3.Distance(attackPosition1, cameraTransform.position) < Vector3.Distance(attackPosition2, cameraTransform.position))
        {
            attackPosition = attackPosition1;
        }
        else
        {
            attackPosition = attackPosition2;
        }
    }

    void FocusCamera()
    {
        // Look at the midpoint of the battle
        Quaternion targetRotation = Quaternion.LookRotation(battleMidpoint - cameraTransform.position);
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, targetRotation, Time.deltaTime * movementTime);

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, attackPosition, Time.deltaTime * movementTime);
        doneBattle = false;
    }

    public void Defocus()
    {
        attackCameraActive = false;
        defocusing = true;
    }

    void DefocusCamera()
    {
        //Debug.Log("returning");
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraReturnPosition, Time.deltaTime * movementTime);
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, cameraReturnRotation, Time.deltaTime * movementTime);
    }

    void HandleMovementInput()
    {
        #region Panning
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
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
        #endregion

        #region Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        #endregion

        #region Zoom
        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }
        #endregion

        // Interpolate from old position and new position to smooth movement
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(attackPosition, 1);

    }
}
