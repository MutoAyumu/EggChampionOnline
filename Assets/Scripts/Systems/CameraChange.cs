using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���I�����ɕʂ̗����t�H���[����ׂ̃N���X
/// </summary>
public class CameraChange : MonoBehaviour
{
    [SerializeField] LevelUpSystem[] _eggs = default;
    LevelUpSystem _currentEgg = default;
    int _eggNumber = 0;
    [SerializeField] CinemachineVirtualCamera _vcam = default;

    private void Start()
    {
        _currentEgg = _eggs[0];
    }

    public void RightChange()
    {
        if (_currentEgg != _eggs[_eggs.Length - 1])
        {
            //��O��Canvas������
            _eggs[_eggNumber].OnCanvasActive();

            _eggNumber++;
            _vcam.Follow = _eggs[_eggNumber].transform;
            _currentEgg = _eggs[_eggNumber];

            _eggs[_eggNumber].OnCanvasActive();
        }
    }    
    public void LeftChange()
    {
        if(_currentEgg != _eggs[0])
        {
            //��O��Canvas������
            _eggs[_eggNumber].OnCanvasActive();

            _eggNumber--;
            _vcam.Follow = _eggs[_eggNumber].transform;
            _currentEgg = _eggs[_eggNumber];

            _eggs[_eggNumber].OnCanvasActive();
        }
    }
}
