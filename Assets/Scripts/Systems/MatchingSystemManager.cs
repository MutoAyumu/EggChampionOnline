using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

/// <summary>
/// �v���C���[���m���}�b�`���O������ׂ̃N���X
/// </summary>
public class MatchingSystemManager : MonoBehaviourPunCallbacks
{
    [SerializeField] byte _maxPlayers;
    [SerializeField] SceneLoad _sceneLoad = default;
    [SerializeField] string _nextSceneName = "InGameScene";

    /// <summary>
    /// �l�b�g���[�N����ؒf����
    /// </summary>
    public void OnDisconnect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
            Debug.Log("�ؒf���܂���");
        }
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
        _sceneLoad.LoadScene(_nextSceneName);

        // ���[���������ɂȂ�����A�ȍ~���̃��[���ւ̎Q����s���ɂ���
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
}
