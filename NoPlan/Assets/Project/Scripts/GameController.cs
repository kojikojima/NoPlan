using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        var player = Instantiate(playerPrefab);

        Observable.EveryLateUpdate()
            .Subscribe(_ =>
            {
                mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, mainCamera.transform.position.z);
            }).AddTo(this);
    }
}
