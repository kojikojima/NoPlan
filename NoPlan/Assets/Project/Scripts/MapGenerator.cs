using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject mapPrefab;

    private void Start()
    {
        Instantiate(mapPrefab);
    }
}
