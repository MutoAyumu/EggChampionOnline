using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// プレイヤー同士をマッチングさせる為のクラス
/// </summary>
public class MatchingSystemManager : MonoBehaviourPunCallbacks
{
    [SerializeField] byte _maxPlayers = 2;
    /// <summary>
    /// マッチング開始時に呼ぶの関数
    /// </summary>
    public void OnQuickMatching()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    /// <summary>
    /// マスターサーバーに接続されたときに呼ばれるコールバック
    /// </summary>
    public override void OnConnectedToMaster()
    {
        //ランダムなルームに参加
        PhotonNetwork.JoinRandomRoom();
    }
    /// <summary>
    /// ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    /// </summary>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // ルームの参加人数を2人に設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = _maxPlayers;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }
    /// <summary>
    /// インゲームのサーバーに接続したときに呼ばれるコールバック
    /// </summary>
    public override void OnJoinedRoom()
    {
        //プレイヤーのアバターを生成する

        // ルームが満員になったら、以降そのルームへの参加を不許可にする
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
}
