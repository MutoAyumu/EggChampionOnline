using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

/// <summary>
/// プレイヤー同士をマッチングさせる為のクラス
/// </summary>
public class MatchingSystemManager : MonoBehaviourPunCallbacks
{
    [SerializeField] byte _maxPlayers;
    [SerializeField] SceneLoad _sceneLoad = default;
    [SerializeField] string _nextSceneName = "InGameScene";

    /// <summary>
    /// ネットワークから切断する
    /// </summary>
    public void OnDisconnect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
            Debug.Log("切断しました");
        }
    }

    /// <summary>
    /// クイックマッチ時に呼ぶ
    /// </summary>
    public void OnQuickMatch()
    {
        //ランダムなルームに接続
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("マッチングしています");
    }

    /// <summary>
    /// ランダムなルームに接続できなかった時に呼ばれるコールバック
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("ルームに参加できませんでした。ルームを作成します");

        // ルームの参加人数を2人に設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = _maxPlayers;

        //新しいルームの作成
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    /// <summary>
    /// ルームに接続（作成）できたときに呼ばれるコールバック
    /// </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("ルームに参加しました");
        PhotonNetwork.IsMessageQueueRunning = false;
        _sceneLoad.LoadScene(_nextSceneName);

        // ルームが満員になったら、以降そのルームへの参加を不許可にする
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
}
