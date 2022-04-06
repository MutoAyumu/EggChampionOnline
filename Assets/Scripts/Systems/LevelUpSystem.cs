using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 卵のステータスをレベルアップさせる
/// </summary>
public class LevelUpSystem : MonoBehaviour
{
    [SerializeField] TextAsset _csv = default;
    [SerializeField, Tooltip("Xが行　Yが列")] Vector2Int _matrix = default;
    int[,] _levelTable = default;

    [SerializeField] string _avaterName = "Avatar";
    [SerializeField] int _eggNum = 1;

    int _currentLevel = 1;

    [SerializeField] int _maxLevel = 5;

    [SerializeField] int _price = 200;
    [SerializeField] Button _buyButton = default;
    [SerializeField] GameObject _selectPanel = default;
    [SerializeField] GameObject _releasePanel = default;

    [SerializeField] Canvas _canvas = default;

    [SerializeField] TMP_Text _priceText = default;

    [SerializeField] TMP_Text _levelUpMoneyText = default;
    [SerializeField] TMP_Text _levelText = default;
    [SerializeField] TMP_Text _hpText = default;
    [SerializeField] TMP_Text _powerText = default;
    [SerializeField] TMP_Text _speedText = default;

    private void Awake()
    {
        CreateTable();
    }
    private void Start()
    {
        if(GameManager.Instance.Levels[_eggNum - 1] > 0)
        {
            _buyButton.gameObject.SetActive(false);
            _selectPanel.SetActive(true);
            _releasePanel.SetActive(false);
        }
        else
        {
            _buyButton.gameObject.SetActive(true);
            _selectPanel.SetActive(false);
            _releasePanel.SetActive(true);
            _priceText.text = "￥" + _price.ToString();
        }
    }

    void CreateTable()
    {
        //テーブルデータの入れ物を作る
        _levelTable = new int[_matrix.x, _matrix.y];

        var stR = new StringReader(_csv.text);
        //一行目を捨てる
        var trash = stR.ReadLine();

        if (stR != null)
        {
            for (int i1 = 0; i1 < _matrix.x; i1++)
            {
                var status = stR.ReadLine().Split(',');

                for (int i2 = 0; i2 < _matrix.y; i2++)
                {
                    _levelTable[i1, i2] = int.Parse(status[i2]);
                }
            }
        }
    }

    public void LevelUp()
    {
        //読み込んだCSVの１列レベル行の値が今の所持金よりも多ければ
        if (GameManager.Instance.LoadMoney() >= _levelTable[_currentLevel, 0] && _currentLevel != _matrix.x - 1)
        {
            GameManager.Instance.SaveMoney(_levelTable[_currentLevel, 1]);  //所持金の更新

            _currentLevel++;    //レベルの更新

            GameManager.Instance.SaveLevelDate(_eggNum, _currentLevel);    //更新したレベルをサーバーにセーブ

            OnUiUpdate();
        }
    }

    void OnUiUpdate()
    {
        if (_levelTable[_currentLevel, 0] == _maxLevel)
        {
            _levelText.text = "Max";
        }
        else
        {
            _levelText.text = _levelTable[_currentLevel, 0].ToString();
        }

        _levelUpMoneyText.text = _levelTable[_currentLevel, 1].ToString();
        _hpText.text = _levelTable[_currentLevel, 2].ToString();
        _powerText.text = _levelTable[_currentLevel, 3].ToString();
        _speedText.text = _levelTable[_currentLevel, 4].ToString();
    }

    public void OnCanvasActive()
    {
        if (_canvas.gameObject.activeSelf != true)
        {
            _canvas.gameObject.SetActive(true);
        }
        else
        {
            _canvas.gameObject.SetActive(false);
        }
    }
}
