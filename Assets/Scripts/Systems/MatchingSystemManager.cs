using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// �v���C���[���m���}�b�`���O������ׂ̃N���X
/// </summary>
public class MatchingSystemManager : MonoBehaviourPunCallbacks
{
    [SerializeField] byte _maxPlayers = 2;
    /// <summary>
    /// �}�b�`���O�J�n���ɌĂԂ̊֐�
    /// </summary>
    public void OnQuickMatching()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    /// <summary>
    /// �}�X�^�[�T�[�o�[�ɐڑ����ꂽ�Ƃ��ɌĂ΂��R�[���o�b�N
    /// </summary>
    public override void OnConnectedToMaster()
    {
        //�����_���ȃ��[���ɎQ��
        PhotonNetwork.JoinRandomRoom();
    }
    /// <summary>
    /// �����_���ŎQ���ł��郋�[�������݂��Ȃ��Ȃ�A�V�K�Ń��[�����쐬����
    /// </summary>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // ���[���̎Q���l����2�l�ɐݒ肷��
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = _maxPlayers;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }
    /// <summary>
    /// �C���Q�[���̃T�[�o�[�ɐڑ������Ƃ��ɌĂ΂��R�[���o�b�N
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
}
