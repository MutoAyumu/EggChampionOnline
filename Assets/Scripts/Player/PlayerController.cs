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
    [SerializeField] string _attackInputName = "Fire1"; //��Ő�΂ɕύX���Ȃ����̂���const�Ƃ��ɂ������������H
    [SerializeField] string _diveInputName = "Fire2";
    [SerializeField] string _guardInputName = "Fire3";
    [SerializeField] string _cameraChangeInputName = "CameraChange";

    Quaternion _targetRotation;
    Vector3 _dir;
    IDamage _target;
    bool IsInvincible;

    private void Start()
    {
        //HP�̏����ݒ�
        _currentHp = _maxHp;

        //�J�n���̊e�J�����̃v���C�I���e�B��ݒ�
        _lockOnCamera.Priority = 0;
        _povCamera.Priority = 1;

        //�}�E�X�J�[�\�����\���ɂ���
        _targetRotation = this.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;

        OnStart();
    }
    private void Update()
    {
        OnUpdate();
        Invincible();

        //���u��
        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1, this.transform);
        }

        //�����̓}�l�[�W���[�Ɉړ�������
        if (Input.GetButtonDown(_cameraChangeInputName))
        {
            if(_povCamera.Priority > _lockOnCamera.Priority)
            {
                //���b�N�I���J�����ɐ؂�ւ�
                _lockOnCamera.Priority = 1;
                _povCamera.Priority = 0;
            }
            else
            {
                //POV�J�����ɐ؂�ւ�
                _lockOnCamera.Priority = 0;
                _povCamera.Priority = 1;
            }

        }
    }

    /// <summary>
    /// AttackState����MoveState�ɖ߂�ׂɃA�j���[�V�����g���K�[�Ŏg��
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
    /// ���G��ԂɂȂ邽�߂̊֐�
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
        else if (_count > 0)    //�p�n�����o���Ȃ��悤�ɉ��񂩍U�����ꂽ�疳�G��ԂɂȂ�
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeLimit)   //�^�C�}�[�����~�b�g�ɒB����ƃJ�E���g�����Z�b�g
            {
                _timer = 0;
                _count = 0;
            }

            if (_count >= _damageLimit)   //�Ƃ肠����5�񂭂炢
            {
                IsInvincible = true;
                _timer = 0;
                _anim.SetTrigger("HitLimit");
                ChangeState(_damageState);
            }
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Hit3"))    //�A���ōU�����󂯂����ɖ��G��Ԃɂ���
        {
            IsInvincible = true;
            ChangeState(_damageState);
        }
    }
}
