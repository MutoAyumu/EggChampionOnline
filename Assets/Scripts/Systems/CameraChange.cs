using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卵選択時に別の卵をフォローする為のクラス
/// </summary>
public class CameraChange : MonoBehaviour
{
    [SerializeField] Transform[] _eggs = default;
    Transform _currentEgg = default;
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
            _eggNumber++;
            _vcam.Follow = _eggs[_eggNumber];
            _currentEgg = _eggs[_eggNumber];
        }
    }    
    public void LeftChange()
    {
        if(_currentEgg != _eggs[0])
        {
            _eggNumber--;
            _vcam.Follow = _eggs[_eggNumber];
            _currentEgg = _eggs[_eggNumber];
        }
    }
}
