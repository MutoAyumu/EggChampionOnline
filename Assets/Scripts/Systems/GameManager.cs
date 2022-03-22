using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] byte _maxPlayers;
    [SerializeField] string _nextSceneName = " ";

    [SerializeField] TMP_InputField _nameInputField = default;
    [SerializeField] TMP_Text _nameText = default;
    [SerializeField] GameObject _namePanel = default;
    [SerializeField] GameObject _selectLinePanel = default;

    static string playerNameKey = "noname";

    protected override void OnAwake()
    {
        OnClosedUI();
        _namePanel.SetActive(true);
    }

    private void Start()
    {
        if(!string.IsNullOrEmpty(_nameInputField.text))
        {
            if(PlayerPrefs.HasKey(playerNameKey))
            {
                _nameInputField.text = PlayerPrefs.GetString(playerNameKey);
                _nameText.text = PlayerPrefs.GetString(playerNameKey);
            }
        }
    }
    /// <summary>
    ///  ネットワークに接続する
    /// </summary>
    public void OnConnect()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("接続しました");
        }
    }
    public void OnDisconnect()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
            Debug.Log("切断しました");
        }
    }
    /// <summary>
    /// UIを非表示にする為の関数
    /// </summary>
    void OnClosedUI()
    {
        _namePanel.SetActive(false);
        _selectLinePanel.SetActive(false);
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
        SceneLoad(_nextSceneName);

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
            PlayerPrefs.SetString(playerNameKey, _nameInputField.text);
            _nameText.text = PlayerPrefs.GetString(playerNameKey);
            Debug.Log(PhotonNetwork.NickName);

            OnClosedUI();
            _selectLinePanel.SetActive(true);
        }
    }
    public void BackNamePanel()
    {
        Debug.Log("NamePanelに戻りました");
        OnClosedUI();
        _namePanel.SetActive(true);
    }
    
    public void SceneLoad(string name)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
    }
}
