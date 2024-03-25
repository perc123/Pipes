using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Define the size of grid cells
    public float cellSize = 1f;

    // Track the occupancy of each grid cell
    private Dictionary<Vector3Int, bool> occupancyMap = new Dictionary<Vector3Int, bool>();

    // Function to check if a grid cell is occupied
    private bool IsCellOccupied(Vector3Int cellPosition)
    {
        bool occupied;
        occupancyMap.TryGetValue(cellPosition, out occupied);
        return occupied;
    }

    // Function to update occupancy of a grid cell
    private void SetCellOccupancy(Vector3Int cellPosition, bool occupied)
    {
        if (occupancyMap.ContainsKey(cellPosition))
            occupancyMap[cellPosition] = occupied;
        else
            occupancyMap.Add(cellPosition, occupied);
    }

    // Function to move the game object and check for free cells
    public void MoveAndCheck(Vector3 movementDirection)
    {
        Vector3Int currentCell = GetGridCell(transform.position);
        Vector3Int nextCell = GetGridCell(transform.position + movementDirection * cellSize);

        if (!IsCellOccupied(nextCell))
        {
            transform.position += movementDirection * cellSize;
            SetCellOccupancy(currentCell, false);
            SetCellOccupancy(nextCell, true);
            Debug.Log("Moved to next cell.");
        }
        else
        {
            Debug.Log("Next cell is occupied. Stopping movement.");
            // Stop movement or take other action
        }
    }

    // Function to convert world position to grid cell position
    private Vector3Int GetGridCell(Vector3 position)
    {
        return new Vector3Int(Mathf.FloorToInt(position.x / cellSize),
            Mathf.FloorToInt(position.y / cellSize),
            Mathf.FloorToInt(position.z / cellSize));
    }
}