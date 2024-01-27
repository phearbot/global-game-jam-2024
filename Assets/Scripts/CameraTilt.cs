using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTilt : MonoBehaviour
{   
    private Player player;
    public float tiltMultiplier = 10; // This variable is affecting where the "clamp" is.
    public float rotationMoveSpeed = 5.0f;
    public float rotationReturnSpeed = 1.0f;


    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {   
        Vector3 forceVector = player.forceVector;

        if (forceVector != Vector3.zero)
        {
            // Apply rotation to the camera
            Vector3 targetRotation = new Vector3(-forceVector.z * tiltMultiplier, 0, forceVector.x * tiltMultiplier);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * rotationMoveSpeed);
        }
        else
        {
            // If no input, lerp the rotation back to zero
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationReturnSpeed);
        }
    }
}
