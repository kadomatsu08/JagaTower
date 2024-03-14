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
    private float walkSpeed = 3.0f;

    [SerializeField]
    [Range(0, 100)]
    private float runSpeed = 6.0f;

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

    private Vector3 _cameraForward;
    private Vector3 _cameraRight;

    private float _currentSpeedCoefficient;

    private float _horizontalAxis;

    private IInteractableObject? _interactableObject;
    private bool                 _isGrounded;

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

        _currentSpeedCoefficient = walkSpeed;
    }


    private void Update()
    {
        // 接地判定
        _isGrounded = characterController.isGrounded;

        // 重力の影響を計算する
        CalcGravity();
        
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
            return;
        }
        
        if (_isGrounded)
        {
            _moveDirection.y = jumpPower;
        }
    }

    public void Run()
    {
        _currentSpeedCoefficient = runSpeed;
    }

    public void Walk()
    {
        _currentSpeedCoefficient = walkSpeed;
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
        vec = vec.normalized * _currentSpeedCoefficient;
        _moveDirection.x = vec.x;
        _moveDirection.z = vec.z;

        characterController.Move(_moveDirection * Time.deltaTime);
    }
    
    public void ToggleRun()
    {
        if (_currentSpeedCoefficient == walkSpeed)
        {
            Run();
        }
        else
        {
            Walk();
        }
    }
    
    private void CalcGravity()
    {
        if (OnLadder)
        {
            _moveDirection.y = 0;
            return;
        }
        
        // 空中にいるとき、擬似的に重力をかける
        if (!_isGrounded)
        {
            _moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }
    }
}