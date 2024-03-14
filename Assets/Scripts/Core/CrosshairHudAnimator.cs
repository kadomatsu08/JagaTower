using Developments;
using Edanoue.Rx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// クロスヘアの表示を管理するクラス
/// </summary>
public class CrosshairHudAnimator : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Sprite handImage;

    [SerializeField]
    private Sprite defaultImage;

    [SerializeField]
    private InteractableDetector detector;


    // Start is called before the first frame update
    private void Start()
    {
        detector.IsDetectedInteractObservable
            .Subscribe(this, (x, state) => state.ToggleCrosshair(x))
            .RegisterTo(destroyCancellationToken);
    }

    private void ToggleCrosshair(bool canInteract)
    {
        image.sprite = canInteract ? handImage : defaultImage;
    }
}