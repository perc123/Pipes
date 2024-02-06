using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject pipePrefab; // Prefab of the pipe to be spawned

    void Start()
    {
        // Spawn the first pipe when the game starts
        SpawnNewPipe();
    }

    void OnTriggerEnter(Collider other)
    {
        // Stop the movement of the collided pipe
        RandomMovement randomMovement = GetComponent<RandomMovement>();
        if (randomMovement != null)
        {
            randomMovement.StopMovement();
        }

        // Spawn a new pipe at a random position
        SpawnNewPipe();
    
    }

    void SpawnNewPipe()
    {
        // Randomly generate a position for the new pipe
        Vector3 newPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));

        // Instantiate a new pipe at the random position
        GameObject newPipe = Instantiate(pipePrefab, newPosition, Quaternion.identity);

        // Set a random color for the new pipe
        newPipe.GetComponent<Renderer>().material.color = Random.ColorHSV();
        
        // Make the new pipe a child of the main pipe
        newPipe.transform.parent = transform;
    }
}