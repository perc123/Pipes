using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private GameObject headPrehab;
    private bool _hasCollided; // Ensures only one collision
    private RandomMovement _randomMovement;
    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (!_hasCollided)
        {
            if (other.gameObject.CompareTag("Borders"))
            {
                headPrehab.GetComponent<RandomMovement>().StopMovement();
                // Spawn a new pipe at a random position
                pipePrefab.GetComponent<Pipe>().SpawnNewPipe();
                _hasCollided = true;
                Debug.Log("Border collision");
            }
            else
            {
                headPrehab.GetComponent<RandomMovement>().Rotate();
                _hasCollided = true;
            }

        }
    }
}
