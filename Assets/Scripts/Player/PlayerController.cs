using UnityEngine;
using Cinemachine;

public partial class PlayerController : MonoBehaviour, IDamage
{
    [Header("Component")]
    [SerializeField] Rigidbody _rb = default;
    [SerializeField] Animator _anim = default;

    [Header("Parameter")]
    [SerializeField] float _moveSpeed = 5f;

    [SerializeField] float _rotatePower = 600f;
    [SerializeField] float _animDampTime = 0.2f;

    [SerializeField] float _attackMovePower = 3f;
    [SerializeField] float _attackPower = 1f;

    [SerializeField] float _knockBackPower = 2f;

    [SerializeField] float _divePower = 10f;

    [SerializeField] float _maxHp = 10f;
    float _currentHp;

    [SerializeField] float _timeLimit = 5f;
    float _timer;

    [SerializeField] int _damageLimit = 5;
    int _count;

    [Header("Objects")]
    [SerializeField] GameObject _barrierObj = default;

    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera _povCamera = default;
    [SerializeField] CinemachineVirtualCamera _lockOnCamera = default;

    [Header("KeyBindingsName")]
    [SerializeField] string _attackInputName = "Fire1"; //後で絶対に変更しないものってconstとかにした方がいい？
    [SerializeField] string _diveInputName = "Fire2";
    [SerializeField] string _guardInputName = "Fire3";
    [SerializeField] string _cameraChangeInputName = "CameraChange";

    Quaternion _targetRotation;
    Vector3 _dir;
    IDamage _target;
    bool IsInvincible;

    private void Start()
    {
        //HPの初期設定
        _currentHp = _maxHp;

        //開始時の各カメラのプライオリティを設定
        _lockOnCamera.Priority = 0;
        _povCamera.Priority = 1;

        //マウスカーソルを非表示にする
        _targetRotation = this.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;

        OnStart();
    }
    private void Update()
    {
        OnUpdate();
        Invincible();

        //仮置き
        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1, this.transform);
        }

        //ここはマネージャーに移動させる
        if (Input.GetButtonDown(_cameraChangeInputName))
        {
            if(_povCamera.Priority > _lockOnCamera.Priority)
            {
                //ロックオンカメラに切り替え
                _lockOnCamera.Priority = 1;
                _povCamera.Priority = 0;
            }
            else
            {
                //POVカメラに切り替え
                _lockOnCamera.Priority = 0;
                _povCamera.Priority = 1;
            }

        }
    }

    /// <summary>
    /// AttackStateからMoveStateに戻る為にアニメーショントリガーで使う
    /// </summary>
    public void ResetState()
    {
        ChangeState(_moveState);
    }

    private void OnTriggerEnter(Collider other)
    {
        _target = other.GetComponent<IDamage>();
    }
    private void OnTriggerExit(Collider other)
    {
        _target = null;
    }

    /// <summary>
    /// 無敵状態になるための関数
    /// </summary>
    void Invincible()
    {
        if (IsInvincible)
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeLimit)
            {
                _timer = 0;
                _count = 0;
                IsInvincible = false;
            }
        }
        else if (_count > 0)    //角ハメが出来ないように何回か攻撃されたら無敵状態になる
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeLimit)   //タイマーがリミットに達するとカウントをリセット
            {
                _timer = 0;
                _count = 0;
            }

            if (_count >= _damageLimit)   //とりあえず5回くらい
            {
                IsInvincible = true;
                _timer = 0;
                _anim.SetTrigger("HitLimit");
                ChangeState(_damageState);
            }
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Hit3"))    //連続で攻撃を受けた時に無敵状態にする
        {
            IsInvincible = true;
            ChangeState(_damageState);
        }
    }
}
