using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // List to store instantiated prefabs
    public List<GameObject> instantiatedPrefabs = new List<GameObject>();
    public List<Vector3> prefabPositions = new List<Vector3>();

    //[SerializeField] private GameObject prefabHead;
    //[SerializeField] private GameObject prefabBody;

    //private RandomMovement _randomMovement;2
    private void Awake()
    {
        instantiatedPrefabs.Clear();
        prefabPositions.Clear();
    }

    void Update()
    {
        AddPrefabPosition();
    }

    // Method to add prefab positions to the list
    public void AddPrefabPosition()
    {
        foreach (GameObject prefab in instantiatedPrefabs)
        {
            prefabPositions.Add(prefab.transform.position);
        }
    }
    
    /* When a rotation of the head is happening, all the prefabs added to the list
     * change their layer status to be able to collide with the head.
     * */
 
    
}