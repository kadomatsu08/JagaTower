#nullable enable
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// FPS視点でプレイヤーを操作するためのクラス
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class FpsController : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController = null!;

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
    
    private float _currentSpeedCoefficient;

    private Vector3 _forward;
    private bool    _isGrounded;

    private Vector3 _moveDirection;
    private Vector3 _right;

    private float _rotationX;

    private Transform _thisTransform = null!;

    private void Start()
    {
        // カーソルの設定
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // transformのキャッシュ
        _thisTransform = transform;
    }

    private void Update()
    {
        // 走っていたら速度を変更する
        if (Input.GetButton("Run"))
        {
            _currentSpeedCoefficient = runSpeed;
        }
        else
        {
            _currentSpeedCoefficient = walkSpeed;
        }

        _forward = _thisTransform.TransformDirection(Vector3.forward);
        _right = _thisTransform.TransformDirection(Vector3.right);
        
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        
        var vec = horizontal * _right + vertical * _forward;
        vec = vec.normalized * _currentSpeedCoefficient;
        _moveDirection.x = vec.x;
        _moveDirection.z = vec.z;

        // 接地判定
        _isGrounded = characterController.isGrounded;;

        if (_isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _moveDirection.y = jumpPower;
            }
        }
        else
        {
            _moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }

        characterController.Move(_moveDirection * Time.deltaTime);

        #region camera rotation
        
        // カメラのピッチ角度を変更する
        _rotationX += -Input.GetAxis("Mouse Y") * verticalRotationSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -cameraPitchLimit, cameraPitchLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        
        // マウスの移動量に応じて体の方向を変える
        transform.rotation *= quaternion.Euler(0, Input.GetAxis("Mouse X") * horizontalRotationSpeed, 0);

        #endregion
    }
}