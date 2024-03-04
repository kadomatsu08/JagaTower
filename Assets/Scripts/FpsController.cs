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
    private float moveSpeed = 3.0f;

    [SerializeField]
    [Range(0, 100)]
    private float jumpPower = 3.0f;

    private Vector3 _moveDirection;

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        var vertical = Input.GetAxis("Vertical") * moveSpeed;
        
        _moveDirection.x = horizontal;
        _moveDirection.z = vertical;

        if (characterController.isGrounded)
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