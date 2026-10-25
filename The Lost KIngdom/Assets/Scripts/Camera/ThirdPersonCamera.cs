using UnityEngine;

namespace LostKingdom
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        public Transform target;
        public float distance = 6f;
        float yaw, pitch = 22f;
        void LateUpdate()
        {
            if (!target) return;
            if (GameManager.Instance.IsPlaying)
            {
                yaw += DemoInput.Look.x * .12f * PlayerPrefs.GetFloat("Sensitivity", 1f);
                pitch = Mathf.Clamp(pitch - DemoInput.Look.y * .12f * PlayerPrefs.GetFloat("Sensitivity", 1f), -15, 65);
            }
            Vector3 pivot = target.position + Vector3.up * 1.45f;
            Vector3 offset = Quaternion.Euler(pitch, yaw, 0) * Vector3.back;
            float safeDistance = distance;
            // Player lives on Ignore Raycast, so this cast cannot hit Aren himself.
            if (Physics.SphereCast(pivot, .23f, offset, out RaycastHit hit, distance, ~((1 << 2)), QueryTriggerInteraction.Ignore))
                safeDistance = Mathf.Max(.3f, hit.distance - .15f);
            Vector3 desired = pivot + offset * safeDistance;
            transform.position = Vector3.Lerp(transform.position, desired, 1 - Mathf.Exp(-18 * Time.unscaledDeltaTime));
            transform.LookAt(pivot);
        }
    }
}
