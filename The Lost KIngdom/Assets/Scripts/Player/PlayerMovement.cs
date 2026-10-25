using UnityEngine;

namespace LostKingdom
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public Transform cameraTransform;
        public float walkSpeed = 4f, runSpeed = 7f, jumpHeight = 1.6f, gravity = -24f;
        CharacterController controller;
        float verticalSpeed, turnVelocity, jumpBuffer, groundedGrace;
        Vector3 spawn;
        void Awake() { controller = GetComponent<CharacterController>(); spawn = transform.position; }
        void Update()
        {
            if (!GameManager.Instance.IsPlaying) return;
            if (DemoInput.Jump) jumpBuffer = .15f; else jumpBuffer -= Time.deltaTime;
            if (controller.isGrounded) groundedGrace = .12f; else groundedGrace -= Time.deltaTime;
            if (controller.isGrounded && verticalSpeed < 0) verticalSpeed = -2f;
            Vector2 input = Vector2.ClampMagnitude(DemoInput.Move, 1);
            Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
            Vector3 right = Vector3.Cross(Vector3.up, forward);
            Vector3 direction = forward * input.y + right * input.x;
            if (direction.sqrMagnitude > .01f)
            {
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnVelocity, .12f), 0);
            }
            if (jumpBuffer > 0 && groundedGrace > 0)
            { verticalSpeed = Mathf.Sqrt(jumpHeight * -2f * gravity); jumpBuffer = 0; groundedGrace = 0; }
            verticalSpeed += gravity * Time.deltaTime;
            controller.Move((direction * (DemoInput.Sprint ? runSpeed : walkSpeed) + Vector3.up * verticalSpeed) * Time.deltaTime);
            if ((controller.collisionFlags & CollisionFlags.Above) != 0 && verticalSpeed > 0) verticalSpeed = 0;
            if (transform.position.y < -10) { controller.enabled = false; transform.position = spawn; controller.enabled = true; verticalSpeed = 0; }
        }
    }
}
