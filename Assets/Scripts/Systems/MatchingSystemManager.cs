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
        //Roomサーバーに参加（存在しないときは生成）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }
    /// <summary>
    /// インゲームのサーバーに接続したときに呼ばれるコールバック
    /// </summary>
    public override void OnJoinedRoom()
    {
        //プレイヤーのアバターを生成する
    }
}
