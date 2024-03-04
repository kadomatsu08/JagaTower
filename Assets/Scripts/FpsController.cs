#nullable enable
using UnityEngine;

/// <summary>
/// FPS視点でプレイヤーを操作するためのクラス
/// 今まだカメラ設定をしていないので、FPS視点ではないです。
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

    private Vector3 _moveDirection;
    private bool    _isGrounded;
    private float   _currentSpeedCoefficient;
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
        
        var horizontal = Input.GetAxis("Horizontal") * _currentSpeedCoefficient;
        var vertical = Input.GetAxis("Vertical") * _currentSpeedCoefficient;
        
        _moveDirection.x = horizontal;
        _moveDirection.z = vertical;

        // 接地判定
        _isGrounded = characterController.isGrounded;
        
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
    }
}