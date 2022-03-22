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
    ///  �l�b�g���[�N�ɐڑ�����
    /// </summary>
    public void OnConnect()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("�ڑ����܂���");
        }
    }
    public void OnDisconnect()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
            Debug.Log("�ؒf���܂���");
        }
    }
    /// <summary>
    /// UI���\���ɂ���ׂ̊֐�
    /// </summary>
    void OnClosedUI()
    {
        _namePanel.SetActive(false);
        _selectLinePanel.SetActive(false);
    }
    /// <summary>
    /// �N�C�b�N�}�b�`���ɌĂ�
    /// </summary>
    public void OnQuickMatch()
    {
        //�����_���ȃ��[���ɐڑ�
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("�}�b�`���O���Ă��܂�");
    }
    /// <summary>
    /// �����_���ȃ��[���ɐڑ��ł��Ȃ��������ɌĂ΂��R�[���o�b�N
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("���[���ɎQ���ł��܂���ł����B���[�����쐬���܂�");

        // ���[���̎Q���l����2�l�ɐݒ肷��
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = _maxPlayers;

        //�V�������[���̍쐬
        PhotonNetwork.CreateRoom(null, roomOptions);
    }
    /// <summary>
    /// ���[���ɐڑ��i�쐬�j�ł����Ƃ��ɌĂ΂��R�[���o�b�N
    /// </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("���[���ɎQ�����܂���");
        PhotonNetwork.IsMessageQueueRunning = false;
        SceneLoad(_nextSceneName);

        // ���[���������ɂȂ�����A�ȍ~���̃��[���ւ̎Q����s���ɂ���
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
    /// <summary>
    /// ���O�o�^���̃{�^���Ŏg��
    /// </summary>
    public void SetName()
    {
        if (!string.IsNullOrEmpty(_nameInputField.text))
        {
            //�v���C���[�̖��O��o�^
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
        Debug.Log("NamePanel�ɖ߂�܂���");
        OnClosedUI();
        _namePanel.SetActive(true);
    }
    
    public void SceneLoad(string name)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
    }
}
