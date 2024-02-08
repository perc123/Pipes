using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMovement : MonoBehaviour
{
    public float speed = 10f; 
    private float _rotationAngle = 90f; 
    private float _rotationTimer; 
    
    [SerializeField] private GameObject mainObjectPrefab;
    [SerializeField] private GameObject cornerObjectPrefab;
    private Color _pipeColor;
    [SerializeField] private GameObject pipePrefab;

    private float _objectLengthY; // Length of the object in the y-axis - Body
    private Vector3 _previousPosition; 
    private bool _mainObjectInstantiated;
    private bool _isMoving = true; 
    private bool _hasCollided;
    private bool _startRotation = true;

    private void Start()
    {
        // Get the length of the object in the y-axis
        _objectLengthY = mainObjectPrefab.GetComponent<Renderer>().bounds.size.y;
        _previousPosition = transform.position; // Store the initial position of the object
        _pipeColor = pipePrefab.GetComponent<Pipe>().SetColor(); // Get the pipe color
        gameObject.GetComponent<Renderer>().material.color = _pipeColor;
    }

    private void Update()
    {
        if (_isMoving)
        {
            float randomDirection = Random.Range(0f, 1f);
            Vector3 movementDirection = randomDirection < 0.5f ? Vector3.up : Vector3.down;
            
            // Move the object in the determined direction with the specified speed
            transform.Translate(movementDirection * (speed * Time.deltaTime));

            // Move the object forward towards the y-axis
            //transform.Translate(Vector3.up * (speed * Time.deltaTime));
            //StartingRotation();

            InstantiateMainObject();

            _rotationTimer += Time.deltaTime;

            if (_rotationTimer >= Random.Range(0.5f, 1.5f))
            {
                Rotate();
                GameObject cornerObject =  Instantiate(cornerObjectPrefab, transform.position, transform.rotation);
                cornerObject.GetComponent<Renderer>().material.color = _pipeColor;
                _rotationTimer = 0f;
            }
        }
    }
    

    private void InstantiateMainObject()
    {
        if (!_mainObjectInstantiated &&
            (Mathf.Abs(transform.position.x - _previousPosition.x) >= _objectLengthY*0.8 ||
             Mathf.Abs(transform.position.y - _previousPosition.y) >= _objectLengthY*0.8 ||
             Mathf.Abs(transform.position.z - _previousPosition.z) >= _objectLengthY*0.8))
        {
            // Instantiate the main object - body
            GameObject mainObject = Instantiate(mainObjectPrefab, transform.position, transform.rotation);

            // Set the color
            mainObject.GetComponent<Renderer>().material.color = _pipeColor;

            // Set the flag to true - ensure that it instantiates one time
            _mainObjectInstantiated = true;

            _previousPosition = transform.position;
        }
        else if (_mainObjectInstantiated &&
                 (Mathf.Abs(transform.position.x - _previousPosition.x) < _objectLengthY*0.8 &&
                  Mathf.Abs(transform.position.y - _previousPosition.y) < _objectLengthY*0.8 &&
                  Mathf.Abs(transform.position.z - _previousPosition.z) < _objectLengthY*0.8))
        {
            _mainObjectInstantiated = false;
        }
    }


    private void OnCollisionEnter()
    {
        if (!_hasCollided)
        {
            // Indicate that the collision has been handled - ensure one collision per frame
            _hasCollided = true;
            StopMovement();
            // Spawn a new pipe at a random position
            pipePrefab.GetComponent<Pipe>().SpawnNewPipe();
        }
    }
    private void Rotate()
    {
        _rotationAngle = ChangeRotationAngle();
        transform.Rotate(Vector3.forward, _rotationAngle);
        transform.Rotate(Vector3.right, _rotationAngle);
    }

    private float ChangeRotationAngle()
    {
        int randomNumber = Random.Range(0, 3);

        switch (randomNumber)
        {
            case 0:
                return 0;
            case 1:
                return 90;
            case 2:
                return -90;
            default:
                return 0;
        }
    }

    public void StopMovement()
    {
        _isMoving = false;
    }
    
    
    //TODO: Random movement towards all axis
    private Vector3 GetRandomDirection()
    {
        // Generate random values for each component of the direction vector
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        // Normalize the vector to ensure constant speed
        Vector3 direction = new Vector3(x, y, z).normalized;

        return direction;
    }

    private Vector3 StartPosition()
    {
        int randomNumber = Random.Range(0, 2);
        Vector3 startPosition = new Vector3();
        switch (randomNumber)
        {
            case 0:
                startPosition = Vector3.down;
                break;
            case 1:
                startPosition = Vector3.up;
                break;
            case 2:
                startPosition = Vector3.right * (speed * Time.deltaTime);
                break;
        }

        return startPosition;
    }
    private void StartingRotation()
    {
        int randomNumber = Random.Range(0, 6);
        _rotationAngle = ChangeRotationAngle();
        
        switch (randomNumber)
        {
            case 0:
                transform.Rotate(Vector3.right, _rotationAngle);
                break;
            case 1:
                transform.Rotate(Vector3.left, _rotationAngle);
                break;
            case 2:
                transform.Rotate(Vector3.right, _rotationAngle);
                break;
            case 3:
                transform.Rotate(Vector3.left, _rotationAngle);
                break;
            case 4:
                transform.Rotate(Vector3.forward, _rotationAngle);
                break;
            case 5:
                transform.Rotate(Vector3.back, _rotationAngle);
                break;
        }

        _startRotation = false;
    }
    // TODO: - Half-size prefab to smoothen corner connections
    void EndingPrefab(Vector3 position, Quaternion rotation)
    {
        // Adjust the scale of the main object to half its length
        Vector3 adjustedScale = mainObjectPrefab.transform.localScale;
        adjustedScale.y *= 0.5f;

        // Determine the axis the main object is moving on
        Vector3 movementDirection = transform.forward; // Assuming the object moves along its forward direction TODO: add other axis movement
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
