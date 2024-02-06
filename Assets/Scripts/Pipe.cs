using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pipe : MonoBehaviour
{
    public GameObject pipePrefab; // Prefab of the pipe to be spawned
    private Color _color;
    void Start()
    {
        // Spawn the first pipe when the game starts
        SpawnNewPipe();
    }


    public void SpawnNewPipe()
    {
        // Randomly generate a position for the new pipe
        Vector3 newPosition = new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), Random.Range(-15f, 15f));

        //Random.InitState(System.DateTime.Now.Millisecond);

        // Instantiate a new pipe at the random position
        GameObject newPipe = Instantiate(pipePrefab, newPosition, Quaternion.identity);

        _color = Random.ColorHSV();
        // Set a random color for the new pipe
        newPipe.GetComponent<Renderer>().material.color = _color;
        Debug.Log(_color);
        // Make the new pipe a child of the main pipe
        //newPipe.transform.parent = transform;
    }

    public Color SetColor()
    {
        return _color;
    }
}