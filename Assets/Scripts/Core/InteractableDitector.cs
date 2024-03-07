using UnityEngine;

namespace Developments
{
    public class InteractableDitector : MonoBehaviour
    {
        [SerializeField]
        private Transform cameraTransform = null!;

        [SerializeField]
        [Range(1, 20)]
        private float rayLength;

        public bool CanInteract { get; private set; }

        // Update is called once per frame
        private void Update()
        {
            var rayDirection = cameraTransform.forward;
            var rayStartPoint = cameraTransform.position;

            #region Debug DrawRay

#if UNITY_EDITOR
            var rayColor = CanInteract ? Color.green : Color.red;
            Debug.DrawRay(rayStartPoint, rayDirection * rayLength, rayColor);
#endif

            # endregion

            // TODO ここのコード終わっています
            // インタラクトの検知と実際に押すところの処理は分離したい気持ちがある
            if (Physics.Raycast(rayStartPoint, rayDirection, out var hit, rayLength))
            {
                if (hit.collider.gameObject.TryGetComponent<IInteractableObject>(out var interactableObject))
                {
                    CanInteract = true;
                    if (Input.GetButtonDown("Fire1"))
                    {
                        interactableObject.OnInteracted();
                    }
                }
                else
                {
                    CanInteract = false;
                }
            }
            else
            {
                CanInteract = false;
            }
        }
    }
}