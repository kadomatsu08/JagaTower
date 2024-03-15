using UnityEngine;

public class KeyBoardDetector : MonoBehaviour
{
    public FpsController _fpsController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _fpsController.Jump();
            _fpsController.IsJumpPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _fpsController.IsJumpPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _fpsController.Run();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _fpsController.Walk();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _fpsController.Fire();
        }

        _fpsController.CameraRotation(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        _fpsController.MoveInput(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
    }
}