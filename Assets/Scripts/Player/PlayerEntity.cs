namespace TheRig.Player
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using TheRig.Gun;

    [RequireComponent(typeof(CharacterController))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] InputActionReference _movement, _lookAround, _jump, _shoot, _reload;
        [SerializeField] Gun _gun;
        [SerializeField] Camera _playerCam;
        [SerializeField] float _speed = .1f;
        [SerializeField] float _rotateSensitivity = .1f;
        [SerializeField] float _camPitchSensitivity = .1f;
        [SerializeField][Range(0, 120)] float _camPitchLimit = 60;
        [SerializeField][Min(1)] float _gravityScale = 9.8f;
        [SerializeField][Min(1)] float _jumpHeight = 10;
        float _groundedGravity = .05f;
        float _yDir;
        CharacterController _controller;

        private void Awake()
        {
            DependencyProvider.Instance.Register(this);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _controller = GetComponent<CharacterController>();
        }

        void Update()
        {

            Vector3 moveDir = Movement();

            Jump();

            _yDir -= Gravity() * Time.deltaTime;

            moveDir.y = _yDir;

            Rotation();

            CamPitch();

            _controller.Move(moveDir * Time.deltaTime);

            Shoot();

            Reload();
        }

        private void Jump()
        {
            if (_jump.action.inProgress && _controller.isGrounded)
            {
                _yDir = _jumpHeight;
            }
        }

        private Vector3 Movement()
        {
            Vector2 movementInput = _movement.action.ReadValue<Vector2>();
            Vector3 moveDir = (transform.forward * movementInput.y * _speed) +
            (transform.right * movementInput.x * _speed);

            return moveDir;
        }

        private float Gravity()
        {
            if (_controller.isGrounded)
            {
                return _groundedGravity;

            }
            else
            {
                return _gravityScale;
            }
        }

        private void Rotation()
        {
            Vector2 lookAroundInput = _lookAround.action.ReadValue<Vector2>();
            transform.rotation = Quaternion.Euler
            (0,
            (transform.rotation.eulerAngles.y + lookAroundInput.x * Time.deltaTime * _rotateSensitivity),
            0);
        }

        private void Shoot()
        {
            if (_shoot.action.inProgress)
            {
                _gun.GetComponent<Gun>().Shoot(_controller.velocity * Time.deltaTime);
            }
        }

        void Reload()
        {
            if (_reload.action.triggered)
            {
                _gun.ForceReload();
            }
        }

        private void CamPitch()
        {
            Vector2 lookAroundInput = _lookAround.action.ReadValue<Vector2>();

            float pitchValue = _playerCam.transform.rotation.eulerAngles.x +
            lookAroundInput.y * Time.deltaTime * _camPitchSensitivity;

            if (pitchValue < _camPitchLimit || pitchValue > 360 - _camPitchLimit)
            {
                _playerCam.transform.localRotation = Quaternion.Euler(pitchValue, 0, 0);
            }
        }
    }
}
