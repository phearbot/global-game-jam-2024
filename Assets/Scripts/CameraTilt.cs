using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTilt : MonoBehaviour
{   
    private Player player;
    public float rotationConstraintX = 10.0f;
    public float rotationConstraintZ = 5.0f;
    public float rotationReturnSpeed = 1.0f;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {   
        Vector3 forceVector = player.forceVector;

        if (forceVector != Vector3.zero)
        {   
            // Apply rotation to the camera
            transform.Rotate(Mathf.Clamp(forceVector.z, -rotationConstraintZ, rotationConstraintZ), 0, Mathf.Clamp(-forceVector.x, -rotationConstraintX, rotationConstraintX), Space.Self);
        }
        else
        {
            // If no input, lerp the rotation back to zero
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationReturnSpeed);
        }
    }
}
