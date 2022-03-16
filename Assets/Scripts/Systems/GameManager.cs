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
    /// UI���\���ɂ���ׂ̊֐�
    /// </summary>
    void OnClosedUI()
    {

    }
    /// <summary>
    /// �N�C�b�N�}�b�`���ɌĂ�
    /// </summary>
    void OnQuickMatch()
    {
        //�����_���ȃ��[���ɐڑ�
        PhotonNetwork.JoinRandomRoom();
    }
    /// <summary>
    /// �����_���ȃ��[���ɐڑ��ł��Ȃ��������ɌĂ΂��R�[���o�b�N
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
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
        //�v���C���[�̃A�o�^�[�𐶐�����

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
        }
    }
}
