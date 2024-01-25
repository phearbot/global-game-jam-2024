using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    // Arena will rotate around x & z axis
    
    public float rotationSpeed = 45f; // Rotation speed in degrees per second
    public float lerpTimeReg = 0.5f; // Lerp time for smooth rotation
    public float lerpTimeFast = 1.0f; // Lerp time for faster rotation
    private float lerpTime;

    // Start is called before the first frame update
    void Start()
    {
        lerpTime = lerpTimeReg;
    }
    
    private void Update()
    {
        RotatePlane();
    }

    private void RotatePlane()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        // Check if shift is held down
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            lerpTime = lerpTimeFast;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            lerpTime = lerpTimeReg;
        }

        Vector3 targetRotation = new Vector3(verticalInput * rotationSpeed, 0f, -horizontalInput * rotationSpeed);

        // Lerp rotation over time
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), lerpTime * Time.deltaTime);

        // If no key is pressed, lerp back to zero rotation
        if (Mathf.Approximately(horizontalInput, 0f) && Mathf.Approximately(verticalInput, 0f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, lerpTime * Time.deltaTime);
        }
    }
}
