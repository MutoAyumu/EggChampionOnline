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
        //Room�T�[�o�[�ɎQ���i���݂��Ȃ��Ƃ��͐����j
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }
    /// <summary>
    /// �C���Q�[���̃T�[�o�[�ɐڑ������Ƃ��ɌĂ΂��R�[���o�b�N
    /// </summary>
    public override void OnJoinedRoom()
    {
        //�v���C���[�̃A�o�^�[�𐶐�����
    }
}
