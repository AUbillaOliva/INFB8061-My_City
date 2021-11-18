using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    public float movementSpeed;
    public float movementTime;

    public float normalSpeed;
    public float fastSpeed;
    public float rotationAmount;

    public float panBorderThickness;
    public Vector2 panLimit;

    public float scrollSpeed;
    public float minY = 5f;
    public float maxY = 120f;

    public Vector3 pos;
    public Quaternion newRotation;
    public Vector3 newZoom;
    public Vector3 zoomAmount;

    private void Start()
    {
        pos = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    void Update()
    {
        HandleInputMovement();
    }

    void HandleInputMovement()
    {
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos += (transform.forward * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos += (transform.forward * -movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos += (transform.right * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos += (transform.right * -movementSpeed * Time.deltaTime);
        }
        

        /*float scroll = Input.GetAxis("Mouse ScrollWheel");
        float scrollPos = scroll * scrollSpeed * 100f * Time.deltaTime;
        pos.y += -scrollPos;
        if (pos.y - scrollPos <= minY + 10f)
        {
            pos.y += scrollPos;
            movementSpeed = normalSpeed;
            Debug.Log("normalSpeed");
        } else
        {
            movementSpeed = fastSpeed;
            Debug.Log("fastSpeed");
        }*/

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        if(Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKey(KeyCode.R))
        {
            if(newZoom.y >= minY)
            {
                newZoom += zoomAmount;
            }
        } 
        if (Input.GetKey(KeyCode.F))
        {
            if (newZoom.y <= maxY )
            {
                newZoom -= zoomAmount;
            }
        }

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationAmount);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
