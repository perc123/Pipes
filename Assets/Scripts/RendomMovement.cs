using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMovement : MonoBehaviour
{
    public float speed = 10f; 
    private float _rotationAngle = 90f; 
    private float _rotationTimer;

    [SerializeField] private GameObject mainObjectPrefab;
    [SerializeField] private GameObject cornerObjectPrefab;
    [SerializeField] private GameObject cornerObjectPrefab2;
    [SerializeField] private GameObject sphereBegin;

    private Color _pipeColor;
    [SerializeField] private GameObject pipePrefab;

    private float _objectLengthY; // Length of the object in the y-axis - Body
    private Vector3 _previousPosition; 
    private bool _mainObjectInstantiated;
    private bool _isMoving = true; 

    private void Start()
    {
        Rotate();
        // Get the length of the object in the y-axis
        _objectLengthY = mainObjectPrefab.GetComponent<Renderer>().bounds.size.y;
        _previousPosition = transform.position; // Store the initial position of the object
        _pipeColor = pipePrefab.GetComponent<Pipe>().GetColor(); // Get the pipe color
        gameObject.GetComponent<Renderer>().material.color = _pipeColor;
        GameObject sphere = Instantiate(sphereBegin, transform.position, Quaternion.identity);
        sphere.GetComponent<Renderer>().material.color = _pipeColor;
    }

    private void Update()
    {
        if (_isMoving)
        {
            // Move the object forward towards the y-axis
            transform.Translate(Vector3.up * (speed * Time.deltaTime));


            _rotationTimer += Time.deltaTime * 10;
            


            if (_rotationTimer % 10 >= Random.Range(5, 11))
            {
                Rotate();
                GameObject cornerObject = Instantiate(cornerObjectPrefab, transform.position, transform.rotation);
                GameObject childObject = Instantiate(cornerObjectPrefab2, transform.position, transform.rotation);

                cornerObject.GetComponent<Renderer>().material.color = _pipeColor;
                SetChildObjectColor(childObject, _pipeColor);
                _rotationTimer = 0;
            }
            InstantiateMainObject();

        }
    }


    private void InstantiateMainObject()
    {
        if (!_mainObjectInstantiated &&
            (Mathf.Abs(transform.position.x - _previousPosition.x) >= _objectLengthY * 0.8 ||
             Mathf.Abs(transform.position.y - _previousPosition.y) >= _objectLengthY * 0.8 ||
             Mathf.Abs(transform.position.z - _previousPosition.z) >= _objectLengthY * 0.8))
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
                 (Mathf.Abs(transform.position.x - _previousPosition.x) < _objectLengthY * 0.8 &&
                  Mathf.Abs(transform.position.y - _previousPosition.y) < _objectLengthY * 0.8 &&
                  Mathf.Abs(transform.position.z - _previousPosition.z) < _objectLengthY * 0.8))
        {
            _mainObjectInstantiated = false;
        }
    }

    public void Rotate()
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

    private void SetChildObjectColor(GameObject childObject, Color color)
    {
        Transform newChild = childObject.transform.GetChild(0);
        // Check if the childObject has the tag "childObject"
        if (newChild.CompareTag("childObject"))
        {
            newChild.GetComponent<Renderer>().material.color = color;
        }
    }

    public void StopMovement()
    {
        _isMoving = false;
    }
}
