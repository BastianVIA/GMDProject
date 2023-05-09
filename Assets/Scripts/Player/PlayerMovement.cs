using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float lookSpeed = 0.15f;
        [SerializeField] private float jumpHeight = 5.0f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float stepInterval = 0.5f;
        [SerializeField] private AudioSource footStepAudio;
        [SerializeField] private AudioSource jumpAudio;
        
        private Animator animator;
        private Inputs input;
        private Vector2 moveVector = Vector2.zero;
        private GameObject cameraObject;
        private Rigidbody rigidbody;
        private bool jumpPressed;
        private float timer;

        private void Awake()
        {
            input = new Inputs();
            input.Player.Enable();
            animator = GetComponent<Animator>();

            cameraObject = GameObject.Find("CameraContainer");
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            MovePlayer();
            Jump();
        }

        private void MovePlayer()
        {
            Vector3 movement = new Vector3(moveVector.x, 0, moveVector.y);

            if (IsMoving(movement))
            {
                string state = movement.z > 0 ? "IsMovingForward" : "IsMovingBackward";
                animator.SetBool(state, true);

                PlayFootStepSounds();

                if (Mouse.current.rightButton.isPressed)
                {
                    animator.SetFloat("Direction", movement.x);
                    var cameraRotation = cameraObject.transform.rotation.eulerAngles;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(cameraRotation), lookSpeed);
                    transform.Translate(movement * (moveSpeed * Time.deltaTime), Space.Self);
                }
                else
                {
                    var rotation = transform.rotation;
                    var direction = rotation.eulerAngles;
                    direction.y += (input.Player.Move.ReadValue<Vector2>().x * lookSpeed * Time.deltaTime);
                    rotation = Quaternion.Slerp(rotation, Quaternion.Euler(direction), lookSpeed);
                    transform.rotation = rotation;
                    transform.position += transform.forward * (input.Player.Move.ReadValue<Vector2>().y * moveSpeed * Time.deltaTime);
                }
            }
            else
            {
                animator.SetBool("IsMovingForward", false);
                animator.SetBool("IsMovingBackward", false);
            }
        }

        private void PlayFootStepSounds()
        {
            if (!IsGrounded())
            {
                return;
            }

            if (timer > stepInterval)
            {
                footStepAudio.Play();
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        private static bool IsMoving(Vector3 movement)
        {
            return movement != Vector3.zero;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveVector = context.ReadValue<Vector2>();
        }

        private void Jump()
        {
            if (jumpPressed && IsGrounded())
            {
                animator.SetBool("IsJumping", true);
                jumpAudio.Play();
                rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
            else if (IsGrounded())
            {
                animator.SetBool("IsJumping", false);
            }

            jumpPressed = false;
        }

        public void OnJump()
        {
            jumpPressed = true;
        }

        private bool IsGrounded()
        {
            return !Physics.Raycast(transform.position, Vector3.down, 0.6f, groundLayer);
        }
    }
}