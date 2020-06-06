using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        var player = Instantiate(playerPrefab);
        mainCamera.transform.parent = player.transform;
    }
}
