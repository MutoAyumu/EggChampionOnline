using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// シーン遷移するときにフェードする
/// </summary>
public class SceneLoad : Singleton<SceneLoad>
{
    [SerializeField] Image _fadeOut = default;
    [SerializeField] Image _fadeIn = default;
    [SerializeField] float _fadeTime = 1f;
    [SerializeField] bool isStartFade = true;

    private void Start()
    {
        if(isStartFade)
        {
            DOVirtual.Float(_fadeIn.fillAmount, 0, _fadeTime, value => _fadeIn.fillAmount = value)
                .OnStart(() =>
                {
                    _fadeIn.gameObject.SetActive(true);
                })
                .OnComplete(() =>
                {
                    _fadeIn.gameObject.SetActive(false);
                });
        }
        else
        {
            _fadeIn.gameObject.SetActive(false);
        }
    }

    public void LoadScene(string name)
    {
        DOVirtual.Float(_fadeOut.fillAmount, 1.0f, _fadeTime, value => _fadeOut.fillAmount = value)
            .OnStart(() =>
            {
                _fadeOut.gameObject.SetActive(true);
            })
            .OnComplete(() =>
            {
                SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
            });
    }
}
