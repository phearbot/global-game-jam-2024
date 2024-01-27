using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveForce = 1.0f;
    public float maxSpeed = 5.0f;  
    public float lerpSpeed = 2.0f; 
    public float currentSpeed = 0.0f;
    Rigidbody rb;
    public Vector3 forceVector;




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        Move(forwardInput, horizontalInput);
    }

    public void Move(float forwardInput, float horizontalInput)
    {
        // Check if shift is held down for speed boost
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveForce = 3.0f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveForce = 1.0f;
        }

        Vector3 forwardForce = new Vector3(0, 0, moveForce * forwardInput);
        Vector3 horizontalForce = new Vector3(moveForce * horizontalInput, 0, 0);
        forceVector = forwardForce + horizontalForce;

        // Lerp the current speed towards the target speed based on whether there is input
        float targetSpeed = forceVector.magnitude * maxSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * lerpSpeed);

        // Apply the force using the lerped speed
        rb.AddForce(forceVector.normalized * currentSpeed, ForceMode.Impulse);
    }
}
