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

        // Update is called once per frame
        private void Update()
        {
            var rayDirection = cameraTransform.forward;
            var rayStartPoint = cameraTransform.position;

            RaycastHit hit;
            var isHit = Physics.Raycast(rayStartPoint, rayDirection, out hit, rayLength);

#if UNITY_EDITOR
            var rayColor = isHit ? Color.green : Color.red;
            Debug.DrawRay(rayStartPoint, rayDirection * rayLength, rayColor);
#endif
        }
    }
}