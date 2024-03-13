# nullable enable

using Edanoue.Rx;
using UnityEngine;

namespace Developments
{
    public class InteractableDetector : MonoBehaviour
    {
        [SerializeField]
        private Transform cameraTransform = null!;

        [SerializeField]
        [Range(1, 20)]
        private float rayLength;

        private IInteractableObject? _interactableObject;

        private ReactiveProperty<bool> IsDetected { get; } = new();

        /// <summary>
        /// インタラクト可能なオブジェクトが検知されているかを通知するObservable
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsDetectedInteractObservable => IsDetected;

        public IInteractableObject? DetectedInteractableObject => _interactableObject;

        private void Awake()
        {
            IsDetected.RegisterTo(destroyCancellationToken);
        }

        // Update is called once per frame
        private void Update()
        {
            IsDetected.Value = DetectInteractable(out _interactableObject);
        }

        /// <summary>
        /// カメラの正面方向にレイを飛ばし、インタラクト可能なオブジェクトが検知されているかを判定する
        /// </summary>
        /// <param name="interactableObject"></param>
        /// <returns></returns>
        private bool DetectInteractable(out IInteractableObject? interactableObject)
        {
            var rayDirection = cameraTransform.forward;
            var rayStartPoint = cameraTransform.position;
            interactableObject = null!;

            #region Debug DrawRay

#if UNITY_EDITOR
            var rayColor = IsDetected.Value ? Color.green : Color.red;
            Debug.DrawRay(rayStartPoint, rayDirection * rayLength, rayColor);
#endif

            # endregion

            // インタラクト可能なオブジェクトが検知されているかを判定
            if (!Physics.Raycast(rayStartPoint, rayDirection, out var hit, rayLength))
            {
                return false;
            }

            if (!hit.collider.gameObject.TryGetComponent<IInteractableObject>(out var interactable))
            {
                return false;
            }

            interactableObject = interactable;
            return true;
        }
    }
}