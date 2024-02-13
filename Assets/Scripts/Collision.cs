using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private GameObject headPrehab;
    private bool _hasCollided; // Ensures only one collision
    private RandomMovement _randomMovement;
    private void OnCollisionEnter()
    {
        if (!_hasCollided)
        {
            headPrehab.GetComponent<RandomMovement>().StopMovement();
            // Spawn a new pipe at a random position
            pipePrefab.GetComponent<Pipe>().SpawnNewPipe();
            _hasCollided = true;
        }
    }
}
