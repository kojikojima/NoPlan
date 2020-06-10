using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Enemy  enemyPrefab;
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        var player = Instantiate(playerPrefab);
        var enemy  = Instantiate(enemyPrefab);

        player.transform.position = new Vector3(-10f, 0f, 0f);
        enemy.transform.position = new Vector3( 10f, 0f, 0f);

        Observable.EveryLateUpdate()
            .Subscribe(_ =>
            {
                mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, mainCamera.transform.position.z);
            }).AddTo(this);
    }
}
