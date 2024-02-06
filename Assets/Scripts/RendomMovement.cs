using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float speed = 5.0f; // Default speed value
    private float rotationAngle = 90f; // Rotation angle in degrees

    private float rotationTimer = 0f; // Timer to track the time elapsed since the last rotation
    
    [SerializeField] private GameObject mainObject;
    [SerializeField] private GameObject cornerObject;

    private float objectLengthY; // Length of the object in the y-axis
    private Vector3 previousPosition; // Previous position of the object
    private bool mainObjectInstantiated = false; // Flag to track if the mainObject is instantiated
    private Vector3 direction = Vector3.up;
    private bool startingPrefabInstantiated = false;
    private bool isMoving = true; // Flag to indicate if the object is moving

    private void Start()
    {
        // Get the length of the object in the y-axis
        objectLengthY = GetComponent<Renderer>().bounds.size.y;
        previousPosition = transform.position; // Store the initial position of the object
    }

    private void Update()
    {
        if (isMoving)
        {
            // Move the object forward towards the y-axis
            transform.Translate(direction * speed * Time.deltaTime);

            // Update the rotation timer
            rotationTimer += Time.deltaTime;
        
            InstantiatePrefab();

            // Check if it's time to rotate the object
            if (rotationTimer > Random.Range(1,4))
            { 
                //EndingPrefab(transform.position,transform.rotation);
                Rotate();
                Instantiate(cornerObject, (transform.position), transform.rotation);
                // Reset the rotation timer
                StartingPrefab();
                rotationTimer = 0f;
            }
        }
       
    }
    private void InstantiatePrefab()
    {
        // Check if the main object has not been instantiated and if the position exceeds its length
        if (!mainObjectInstantiated &&
            (Mathf.Abs(transform.position.x - previousPosition.x) >= objectLengthY ||
             Mathf.Abs(transform.position.y - previousPosition.y) >= objectLengthY  ||
             Mathf.Abs(transform.position.z - previousPosition.z) >= objectLengthY ))
        {
            // Instantiate the main object
            Instantiate(mainObject, transform.position, transform.rotation);
        
            // Set the flag to true
            mainObjectInstantiated = true;

            // Reset the previous position
            previousPosition = transform.position;

        }
        else if (mainObjectInstantiated &&
                 (Mathf.Abs(transform.position.x - previousPosition.x) < objectLengthY &&
                  Mathf.Abs(transform.position.y - previousPosition.y) < objectLengthY &&
                  Mathf.Abs(transform.position.z - previousPosition.z) < objectLengthY))
        {
            // If the position is within the length, reset the flag
            mainObjectInstantiated = false;
        }
    }

    void StartingPrefab()
    {
        Vector3 adjustedScale = mainObject.transform.localScale;
        adjustedScale.y *= 1.5f;
        // Check if the main object has not been instantiated and if the position exceeds its length
        if (!mainObjectInstantiated &&
            (Mathf.Abs(transform.position.x - previousPosition.x) >= objectLengthY ||
             Mathf.Abs(transform.position.y - previousPosition.y) >= objectLengthY   ||
             Mathf.Abs(transform.position.z - previousPosition.z) >= objectLengthY  ))
        {

            // Instantiate the main object
            //Instantiate(mainObject, transform.position, transform.rotation);
            GameObject mainObjectHalfLength = Instantiate(mainObject, transform.position, transform.rotation);
            mainObjectHalfLength.transform.localScale = adjustedScale;
            // Set the flag to true
            mainObjectInstantiated = true;
            Debug.Log("This");
            
            // Reset the previous position
            previousPosition = transform.position;

        }
        else if (mainObjectInstantiated &&
                 (Mathf.Abs(transform.position.x - previousPosition.x) < objectLengthY &&
                  Mathf.Abs(transform.position.y - previousPosition.y) < objectLengthY &&
                  Mathf.Abs(transform.position.z - previousPosition.z) < objectLengthY ))
        {
            // If the position is within the length, reset the flag
            mainObjectInstantiated = false;
        }
     
    }
    
    void EndingPrefab(Vector3 position, Quaternion rotation)
    {
        // Adjust the scale of the main object to half its length
        Vector3 adjustedScale = mainObject.transform.localScale;
        adjustedScale.y *= 0.5f;

        // Determine the axis the main object is moving on
        Vector3 movementDirection = transform.forward; // Assuming the object moves along its forward direction
        if (Mathf.Abs(Vector3.Dot(transform.right, movementDirection)) > 0.9f) // Check if moving mainly along the right axis
        {
            // Instantiate the main object with the adjusted scale at y + 0.5 on the right axis
            GameObject mainObjectHalfLength = Instantiate(mainObject, transform.position - Vector3.right * 0.5f, transform.rotation);
            mainObjectHalfLength.transform.localScale = adjustedScale;
            Debug.Log("Here Ending1");

        }
        else if (Mathf.Abs(Vector3.Dot(transform.up, movementDirection)) > 0.9f) // Check if moving mainly along the up axis
        {
            // Instantiate the main object with the adjusted scale at y + 0.5 on the up axis
            GameObject mainObjectHalfLength = Instantiate(mainObject, transform.position - Vector3.up * 0.5f, transform.rotation);
            mainObjectHalfLength.transform.localScale = adjustedScale;
            Debug.Log("Here Ending2");

        }
        else if (Mathf.Abs(Vector3.Dot(transform.forward, movementDirection)) > 0.9f) // Check if moving mainly along the forward axis
        {
            // Instantiate the main object with the adjusted scale at y + 0.5 on the forward axis
            GameObject mainObjectHalfLength = Instantiate(mainObject, transform.position - Vector3.up * 0.5f, transform.rotation);
            mainObjectHalfLength.transform.localScale = adjustedScale;
            Debug.Log("Here Ending3");

        }
    }

    
    private void Rotate()
    {
        // Get random rotation angle
        rotationAngle = ChangeRotationAngle();
        transform.Rotate(Vector3.forward, rotationAngle);
        transform.Rotate(Vector3.right, rotationAngle);
    }

    
    // Method to randomly select either -90 or 90 degrees
    private float ChangeRotationAngle()
    {
        // Generate a random integer between 0 and 2
        int randomNumber = Random.Range(0, 3);

        // Map the random integer to one of the desired values
        switch (randomNumber)
        {
            case 0:
                return 0;
            case 1:
                return 90;
            case 2:
                return -90;
            default:
                return 0; // Default to 0 if unexpected value is generated
        }
    }
    public void StopMovement()
    {
        isMoving = false;
    }
}
