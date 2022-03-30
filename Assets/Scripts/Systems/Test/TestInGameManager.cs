using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestInGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> _insPos = new List<Transform>();
    [SerializeField] string _playerPrefabName = "Avatar";
    private void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;

        var t = Random.Range(0, _insPos.Count);

        PhotonNetwork.Instantiate(TestManager.Instance.Avater, _insPos[t].position, Quaternion.identity);
        Debug.Log(_insPos[t].name + "Ç…ê∂ê¨ÇµÇ‹ÇµÇΩ");

        _insPos.Remove(_insPos[t]);
    }
}
