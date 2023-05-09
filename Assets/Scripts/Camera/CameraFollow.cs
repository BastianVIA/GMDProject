using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothTime = 0.3f;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float mouseSensitivity = 0.5f;

        private Vector3 velocity = Vector3.zero;
        private Inputs input;

        private void Awake()
        {
            input = new Inputs();
            input.Player.Enable();
        }

        private void Update()
        {
            if (target == null) return;

            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            if (Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed)
            {
                var mousePosition = input.Player.Look.ReadValue<Vector2>();
                var rotation = transform.localEulerAngles;
                rotation.y += mousePosition.x * mouseSensitivity;
                transform.localEulerAngles = rotation;
            }
        }
    }
}