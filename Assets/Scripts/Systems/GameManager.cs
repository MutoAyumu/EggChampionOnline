using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] byte _maxPlayers;

    [SerializeField] InputField _nameInputField = default;
    protected override void OnAwake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    /// <summary>
    /// UIを非表示にする為の関数
    /// </summary>
    void OnClosedUI()
    {

    }
    /// <summary>
    /// クイックマッチ時に呼ぶ
    /// </summary>
    void OnQuickMatch()
    {
        //ランダムなルームに接続
        PhotonNetwork.JoinRandomRoom();
    }
    /// <summary>
    /// ランダムなルームに接続できなかった時に呼ばれるコールバック
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
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
        //プレイヤーのアバターを生成する

        // ルームが満員になったら、以降そのルームへの参加を不許可にする
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
    /// <summary>
    /// 名前登録時のボタンで使う
    /// </summary>
    public void SetName()
    {
        if (!string.IsNullOrEmpty(_nameInputField.text))
        {
            //プレイヤーの名前を登録
            PhotonNetwork.NickName = _nameInputField.text;
        }
    }
}
