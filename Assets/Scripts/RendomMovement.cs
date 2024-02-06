using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMovement : MonoBehaviour
{
    public float speed = 5.0f; // Default speed value
    private float rotationAngle = 90f; // Rotation angle in degrees

    private float rotationTimer = 0f; // Timer to track the time elapsed since the last rotation
    //private Pipe pipe;
    
    [SerializeField] private GameObject mainObjectPrefab;
    [SerializeField] private GameObject cornerObjectPrefab;
    private Color pipeColor;
    [SerializeField] private GameObject pipePrefab;

    private float objectLengthY; // Length of the object in the y-axis
    private Vector3 previousPosition; // Previous position of the object
    private bool mainObjectInstantiated = false; // Flag to track if the mainObject is instantiated
    private bool isMoving = true; // Flag to indicate if the object is moving
    private bool hasCollided = false;

    private void Start()
    {
        // Get the length of the object in the y-axis
        objectLengthY = mainObjectPrefab.GetComponent<Renderer>().bounds.size.y;
        previousPosition = transform.position; // Store the initial position of the object
        pipeColor = pipePrefab.GetComponent<Pipe>().SetColor();
        gameObject.GetComponent<Renderer>().material.color = pipeColor;
        //cornerObjectPrefab.GetComponent<Renderer>().material.color = pipePrefab.GetComponent<Pipe>().SetColor();

    }

    private void Update()
    {
        if (isMoving)
        {
            // Move the object forward towards the y-axis
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            InstantiateMainObject();

            // Update the rotation timer
            rotationTimer += Time.deltaTime;

            // Check if it's time to rotate the object
            if (rotationTimer >= Random.Range(0.5f, 1.5f))
            {
                Rotate();
                GameObject cornerObject =  Instantiate(cornerObjectPrefab, transform.position, transform.rotation);
                cornerObject.GetComponent<Renderer>().material.color = pipeColor;
                rotationTimer = 0f;
            }
        }
    }

    private void InstantiateMainObject()
    {
        // Check if the main object has not been instantiated and if the position exceeds its length
        if (!mainObjectInstantiated &&
            (Mathf.Abs(transform.position.x - previousPosition.x) >= objectLengthY*0.8 ||
             Mathf.Abs(transform.position.y - previousPosition.y) >= objectLengthY*0.8 ||
             Mathf.Abs(transform.position.z - previousPosition.z) >= objectLengthY*0.8))
        {
            // Instantiate the main object
            GameObject mainObject = Instantiate(mainObjectPrefab, transform.position, transform.rotation);

            // Set the color/material of the main object to match the pipe color
            mainObject.GetComponent<Renderer>().material.color = pipeColor;

            // Set the flag to true
            mainObjectInstantiated = true;

            // Reset the previous position
            previousPosition = transform.position;
        }
        else if (mainObjectInstantiated &&
                 (Mathf.Abs(transform.position.x - previousPosition.x) < objectLengthY*0.8 &&
                  Mathf.Abs(transform.position.y - previousPosition.y) < objectLengthY*0.8 &&
                  Mathf.Abs(transform.position.z - previousPosition.z) < objectLengthY*0.8))
        {
            // If the position is within the length, reset the flag
            mainObjectInstantiated = false;
        }
    }


    private void OnCollisionEnter()
    {
        if (!hasCollided)
        {
            // Set the flag to true to indicate that the collision has been handled
            hasCollided = true;
            // Stop the movement of the collided pipe
            StopMovement();
            // Spawn a new pipe at a random position
            pipePrefab.GetComponent<Pipe>().SpawnNewPipe();
            Debug.Log("collided");
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




    void EndingPrefab(Vector3 position, Quaternion rotation)
    {
        // Adjust the scale of the main object to half its length
        Vector3 adjustedScale = mainObjectPrefab.transform.localScale;
        adjustedScale.y *= 0.5f;

        // Determine the axis the main object is moving on
        Vector3 movementDirection = transform.forward; // Assuming the object moves along its forward direction
        if (Mathf.Abs(Vector3.Dot(transform.right, movementDirection)) > 0.9f) // Check if moving mainly along the right axis
        {
            // Instantiate the main object with the adjusted scale at y + 0.5 on the right axis
            GameObject mainObjectHalfLength = Instantiate(mainObjectPrefab, transform.position - Vector3.right * 0.5f, transform.rotation);
            mainObjectHalfLength.transform.localScale = adjustedScale;
            Debug.Log("Here Ending1");

        }
        else if (Mathf.Abs(Vector3.Dot(transform.up, movementDirection)) > 0.9f) // Check if moving mainly along the up axis
        {
            // Instantiate the main object with the adjusted scale at y + 0.5 on the up axis
            GameObject mainObjectHalfLength = Instantiate(mainObjectPrefab, transform.position - Vector3.up * 0.5f, transform.rotation);
            mainObjectHalfLength.transform.localScale = adjustedScale;
            Debug.Log("Here Ending2");

        }
        else if (Mathf.Abs(Vector3.Dot(transform.forward, movementDirection)) > 0.9f) // Check if moving mainly along the forward axis
        {
            // Instantiate the main object with the adjusted scale at y + 0.5 on the forward axis
            GameObject mainObjectHalfLength = Instantiate(mainObjectPrefab, transform.position - Vector3.up * 0.5f, transform.rotation);
            mainObjectHalfLength.transform.localScale = adjustedScale;
            Debug.Log("Here Ending3");

        }
    }

}
