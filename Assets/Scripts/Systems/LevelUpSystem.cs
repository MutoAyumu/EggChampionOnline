using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    [SerializeField] TextAsset _csv = default;
    [SerializeField, Tooltip("X���s�@Y����")] Vector2Int _matrix = default;
    int[,] _levelTable = default;

    [SerializeField] TMP_Text _levelUpMoneyText = default;

    [SerializeField] TMP_Text _levelText = default;
    static int _currentLevel = 0;

    [SerializeField] TMP_Text _hpText = default;
    [SerializeField] TMP_Text _powerText = default;
    [SerializeField] TMP_Text _speedText = default;

    /*ToDo
     ���x���̏���Json�Ƃ��ŕۑ�����悤�ɂ�����
     */

    void Awake()
    {
        //�e�[�u���f�[�^�̓��ꕨ�����
        _levelTable = new int[_matrix.x, _matrix.y];

        var stR = new StringReader(_csv.text);
        //��s�ڂ��̂Ă�
        var trash = stR.ReadLine();

        if(stR != null)
        {
            for(int i1 = 0; i1 < _matrix.x; i1++)
            {
                var status = stR.ReadLine().Split(',');

                for(int i2 = 0; i2 < _matrix.y; i2++)
                {
                    _levelTable[i1, i2] = int.Parse(status[i2]);
                }
            }
        }
    }
    private void Start()
    {
        OnUiUpdate();
    }
    public void OnLevelUp()
    {
        //�ǂݍ���CSV�̂P�񃌃x���s�̒l�����̏����������������
        if(GameManager.Instance.Money >= _levelTable[_currentLevel, 0] && _currentLevel != _matrix.x - 1)
        {
            GameManager.Instance.Money -= _levelTable[_currentLevel, 0];
            GameManager.Instance.MoneyUpdate();
            _currentLevel++;
            OnUiUpdate();
        }
    }
    void OnUiUpdate()
    {
        var level = _currentLevel + 1;
        _levelText.text = level.ToString();
        _levelUpMoneyText.text = _levelTable[_currentLevel, 0].ToString();
        _hpText.text = _levelTable[_currentLevel, 1].ToString();
        _powerText.text = _levelTable[_currentLevel, 2].ToString();
        _speedText.text = _levelTable[_currentLevel, 3].ToString();
    }
}
