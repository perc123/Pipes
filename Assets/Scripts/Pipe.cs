using UnityEngine;
using Random = UnityEngine.Random;

public class Pipe : MonoBehaviour
{
    public GameObject pipePrefab; // Prefab of the pipe to be spawned
    private Color _color;
    void Start()
    { 
        SpawnNewPipe();
    }
    
    public void SpawnNewPipe()
    {
        // Randomly generate a position for the new pipe
        Vector3 newPosition = new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), Random.Range(-15f, 15f));
        
        GameObject newPipe = Instantiate(pipePrefab, newPosition, Quaternion.identity);
        
        // Set a random color for the new pipe
        _color = Random.ColorHSV();
        newPipe.GetComponent<Renderer>().material.color = _color;
        Debug.Log(_color);
    }

    public Color GetColor()
    {
        return _color;
    }
}