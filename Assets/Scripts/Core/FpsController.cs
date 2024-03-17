#nullable enable
using Developments;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// FPS視点でプレイヤーを操作するためのクラス
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class FpsController : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController = null!;

    [SerializeField]
    private InteractableDetector interactableDetector = null!;

    [SerializeField]
    [Range(0, 100)]
    private float baseWalkSpeed = 3.0f;

    [SerializeField]
    [Range(0, 100)]
    private float baseRunSpeed = 6.0f;

    [SerializeField]
    [Range(0, 100)]
    private float jumpPower = 3.0f;

    [SerializeField]
    private Camera playerCamera = null!;

    [SerializeField]
    [Range(0.1f, 5f)]
    private float verticalRotationSpeed;

    [SerializeField]
    [Range(0.1f, 5f)]
    private float horizontalRotationSpeed;

    [SerializeField]
    [Range(60f, 90f)]
    private float cameraPitchLimit = 85.0f;

    [SerializeField]
    [Range(0, 10)]
    private float onLadderClimbSpeed = 1.0f;

    [SerializeField]
    [Range(0, 10)]
    private float onLadderFallingSpeed = 1.0f;

    private Vector3 _cameraForward;
    private Vector3 _cameraRight;
    private float   _currentRunSpeed;

    private float _currentWalkSpeed;

    private float _horizontalAxis;

    private IInteractableObject? _interactableObject;
    private bool                 _isGrounded;

    private bool _isRunPressed;

    private Vector3 _moveDirection;

    private float _rotationX;
    private float _verticalAxis;

    /// <summary>
    /// はしごの近くにいるかどうか
    /// </summary>
    public bool OnLadder { get; set; }

    /// <summary>
    /// ジャンプボタンが押されているかどうか
    /// </summary>
    public bool IsJumpPressed { get; set; }

    private void Awake()
    {
        // カーソルの設定
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // プレイヤーの速度の初期化
        _currentWalkSpeed = baseWalkSpeed;
        _currentRunSpeed = baseRunSpeed;
    }


    private void Update()
    {
        // 接地判定
        _isGrounded = characterController.isGrounded;

        // 上下方向の移動量を計算する
        CalcYAxisMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SpeedUpItem>(out var otherSpeedUpItem))
        {
            otherSpeedUpItem.OnGotItem();
            AddSpeed(1.0f);
        }
    }


    public void CameraRotation(float valueX, float valueY)
    {
        // カメラのピッチ角度を変更する
        _rotationX += -valueY * verticalRotationSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -cameraPitchLimit, cameraPitchLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);

        // 体の方向を変える
        transform.rotation *= quaternion.Euler(0, valueX * horizontalRotationSpeed * Time.deltaTime, 0);
    }

    public void Jump()
    {
        if (OnLadder)
        {
            _moveDirection.y = onLadderClimbSpeed;
            return;
        }

        if (_isGrounded)
        {
            _moveDirection.y = jumpPower;
        }
    }

    public void Run()
    {
        _isRunPressed = true;
    }

    public void Walk()
    {
        _isRunPressed = false;
    }

    public void Fire()
    {
        interactableDetector.DetectedInteractableObject?.OnInteracted();
    }

    // プレイヤーを移動させる
    public void MoveInput(float vertical, float horizontal)
    {
        _verticalAxis = vertical;
        _horizontalAxis = horizontal;

        _cameraForward = playerCamera.transform.forward;
        _cameraRight = playerCamera.transform.right;

        var vec = _horizontalAxis * _cameraRight + _verticalAxis * _cameraForward;
        vec = new Vector3(vec.x, 0, vec.z);

        var speed = _isRunPressed ? _currentRunSpeed : _currentWalkSpeed;

        vec = vec.normalized * speed;
        _moveDirection.x = vec.x;
        _moveDirection.z = vec.z;

        characterController.Move(_moveDirection * Time.deltaTime);
    }

    public void ToggleRun()
    {
        _isRunPressed = !_isRunPressed;
    }

    private void CalcYAxisMovement()
    {
        if (OnLadder)
        {
            if (IsJumpPressed)
            {
                // 一定速度で上昇する
                _moveDirection.y = onLadderClimbSpeed;
            }
            else
            {
                // はしごの近くにいるときは、一定速度で落下する
                // 下方向への移動なので、マイナスをかける
                _moveDirection.y = -onLadderFallingSpeed;
            }

            return;
        }

        // 空中にいるとき、擬似的に重力をかける
        if (!_isGrounded)
        {
            _moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    /// <summary>
    /// 現在の歩き速度、走り速度を上げる
    /// </summary>
    /// <param name="speedUpValue"></param>
    public void AddSpeed(float speedUpValue)
    {
        _currentWalkSpeed += speedUpValue;
        _currentRunSpeed += speedUpValue;
    }

    /// <summary>
    /// 歩き速度、走り速度をベースの速度にリセットする
    /// </summary>
    public void BaseSpeedReset()
    {
        _currentWalkSpeed = baseWalkSpeed;
        _currentRunSpeed = baseRunSpeed;
    }
}