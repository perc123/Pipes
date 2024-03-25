using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private GameObject borders;
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private GameObject headPrehab;
    private bool _hasCollided;    // Ensures only one collision
    private RandomMovement _randomMovement;
    private CollisionBorders CollisionBorders;

    private void Update()
    {
        _hasCollided = false;
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        
        if (!_hasCollided)
        {
            if (collision.gameObject.layer == 3)
            {
                headPrehab.GetComponent<RandomMovement>().StopMovement();
                // Spawn a new pipe at a random position
                pipePrefab.GetComponent<Pipe>().SpawnNewPipe();
                Debug.Log("Border collision");
            }
            else
            { 
                headPrehab.GetComponent<RandomMovement>().Rotate();
                Debug.Log("collision");
            }
            _hasCollided = true;
        }
    }
}
