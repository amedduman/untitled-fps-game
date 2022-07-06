using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerEntity : MonoBehaviour
{
    [SerializeField] InputActionReference _movement, _lookAround, _jump;
    [SerializeField] float _speed = .1f;
    [SerializeField] float _turnSensitivity = .1f;
    [SerializeField] [Min(1)] float _gravityScale = 9.8f;
    [SerializeField] [Min(1)] float _jumpHeight = 10;
    float _groundedGravity = .05f; 
    float _yDir;
    CharacterController _controller;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        Vector3 moveDir = Movement();
        
        Jump();

        _yDir -= Gravity();

        moveDir.y = _yDir;

        Rotation();

        _controller.Move(moveDir * Time.deltaTime);
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
            return  _gravityScale;
        }
    }

    private void Rotation()
    {
        Vector2 lookAroundInput = _lookAround.action.ReadValue<Vector2>();
        transform.rotation = Quaternion.Euler
        (0,
        (transform.rotation.eulerAngles.y + lookAroundInput.x * Time.deltaTime * _turnSensitivity),
        0);
    }
}
