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

    [SerializeField] TMP_InputField _nameInputField = default;
    [SerializeField] TMP_Text _nameText = default;
    [SerializeField] GameObject _namePanel = default;
    [SerializeField] GameObject _selectLinePanel = default;

    static string playerNameKey = "noname";

    protected override void OnAwake()
    {
        Screen.SetResolution(960, 540, false, 60);

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
    /// <summary>
    /// UIを非表示にする為の関数
    /// </summary>
    void OnClosedUI()
    {
        _namePanel.SetActive(false);
        _selectLinePanel.SetActive(false);
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
}
