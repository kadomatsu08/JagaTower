using System;
using UnityEngine;

public class GamePadDetector : MonoBehaviour
{
    public FpsController _fpsController;

    [SerializeField]
    [Tooltip("スティックのデッドゾーン この数値以下の入力は無視される")]
    [Range(0, 1)]
    private float deadZone = 0.3f;

    [SerializeField]
    [Tooltip("カメラのY軸反転" +
             "true: 反転する" +
             "false: 反転しない")]
    private bool yReverse;

    private void Update()
    {
        // ×ボタン
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            _fpsController.Jump();
            _fpsController.IsJumpPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton1))
        {
            _fpsController.IsJumpPressed = false;
        }

        // ○ボタン
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            _fpsController.Fire();
        }

        // 左スティック押し込み
        if (Input.GetKeyDown(KeyCode.JoystickButton10))
        {
            _fpsController.ToggleRun();
            Debug.Log("左スティック押し込み");
        }

        // 左スティック 前後左右
        var inputX = Input.GetAxis("Axis X");
        if (Math.Abs(inputX) < deadZone)
        {
            inputX = 0;
        }

        var inputY = Input.GetAxis("Axis Y");
        if (Math.Abs(inputY) < deadZone)
        {
            inputY = 0;
        }

        // スティックの上下の入力は、上に倒すと -1 下に倒すと 1 になる
        // UnityのInputsystemのVerticalの入力 前進時に 1 後退時に -1にスティックの入力をあわせるため
        // inputYの値にマイナスをかける
        _fpsController.MoveInput(-inputY, inputX);
        Debug.Log($"MOVEinputX: {inputX}, inputY: {inputY}");

        // Rスティック上下左右
        var input3rd = Input.GetAxis("Axis 3rd");
        if (Math.Abs(input3rd) < deadZone)
        {
            input3rd = 0;
        }

        var input6th = Input.GetAxis("Axis 6th");
        if (Math.Abs(input6th) < deadZone)
        {
            input6th = 0;
        }

        var reverse = yReverse ? -1 : 1;
        _fpsController.CameraRotation(input3rd, input6th * reverse);
        Debug.Log($"CAMERAinput3rd: {input3rd}, input6th: {input6th}");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _fpsController.ToggleRun();
        }
    }
}