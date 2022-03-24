using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSystem : MonoBehaviour
{
    [SerializeField] TextAsset _csv = default;
    [SerializeField, Tooltip("Xが行　Yが列")] Vector2Int _matrix = default;
    int[,] _levelTable = default;

    [SerializeField] int _eggNum = 0;
    int _currentLevel;

    [SerializeField] int _price = 200;
    [SerializeField] Button _buyButton = default;
    [SerializeField] GameObject _selectPanel = default;

    [SerializeField] Canvas _canvas = default;

    [SerializeField] TMP_Text _levelUpMoneyText = default;

    [SerializeField] TMP_Text _levelText = default;

    [SerializeField] TMP_Text _hpText = default;
    [SerializeField] TMP_Text _powerText = default;
    [SerializeField] TMP_Text _speedText = default;

    /*ToDo
     レベルの情報をJsonとかで保存するようにしたい
     */

    void Awake()
    {
        //テーブルデータの入れ物を作る
        _levelTable = new int[_matrix.x, _matrix.y];

        var stR = new StringReader(_csv.text);
        //一行目を捨てる
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
        if (LevelUpManager.Buy[_eggNum])
        {
            _currentLevel = LevelUpManager.Levels[_eggNum];
            _buyButton.gameObject.SetActive(false);
            _selectPanel.SetActive(true);
        }

        OnUiUpdate();
    }
    public void OnBuyEgg()
    {
        if(!LevelUpManager.Buy[_eggNum])
        {
            if(GameManager.Instance.Money >= _price)
            {
                GameManager.Instance.MoneyUpdate(_price);
                _currentLevel = LevelUpManager.Instance.LevelUp(_eggNum);

                LevelUpManager.Instance.BuyEgg(_eggNum);
                _buyButton.gameObject.SetActive(false);
                _selectPanel.SetActive(true);
            }
        }
    }
    public void OnLevelUp()
    {
        //読み込んだCSVの１列レベル行の値が今の所持金よりも多ければ
        if(GameManager.Instance.Money >= _levelTable[_currentLevel, 0] && _currentLevel != _matrix.x - 1)
        {
            GameManager.Instance.MoneyUpdate(_levelTable[_currentLevel, 0]);

            _currentLevel = LevelUpManager.Instance.LevelUp(_eggNum);
            OnUiUpdate();
        }
    }
    void OnUiUpdate()
    {
        if (_levelTable[_currentLevel, 0] == 0)
        {
            _levelText.text = "Max";
        }
        else
        {
            var level = _currentLevel + 1;
            _levelText.text = level.ToString();
        }

        _levelUpMoneyText.text = _levelTable[_currentLevel, 0].ToString();
        _hpText.text = _levelTable[_currentLevel, 1].ToString();
        _powerText.text = _levelTable[_currentLevel, 2].ToString();
        _speedText.text = _levelTable[_currentLevel, 3].ToString();
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
