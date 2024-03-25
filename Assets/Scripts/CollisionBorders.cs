using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBorders : MonoBehaviour
{
    
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private GameObject headPrehab;
    public bool _hasCollidedB = false; // Ensures only one collision
    private RandomMovement _randomMovement;
    private void OnCollisionEnter()
    {
        if (!_hasCollidedB)
        {

            
        }
    }
}
