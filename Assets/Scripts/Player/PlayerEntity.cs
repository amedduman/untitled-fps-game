using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerEntity : MonoBehaviour
{
    [SerializeField] InputActionReference _movement, _lookAround;
    [SerializeField] float _speed = .1f;
    [SerializeField] float _turnSensitivity = .1f;
    [SerializeField] float _gravity = 9.8f;
    float _groundedGravity = .05f;
    CharacterController _controller;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 moveDir = Movement();
        moveDir = Gravity(moveDir);
        Rotation();

        _controller.Move(moveDir * _speed * Time.deltaTime);
    }

    private Vector3 Movement()
    {
        Vector2 movementInput = _movement.action.ReadValue<Vector2>();
        Vector3 moveDir = (transform.forward * movementInput.y) +
        (transform.right * movementInput.x);
        return moveDir;
    }

    private Vector3 Gravity(Vector3 moveDir)
    {
        Vector3 gravityDir = new Vector3(0, -1, 0);
        if (_controller.isGrounded)
        {
            moveDir += gravityDir * _groundedGravity;

        }
        else
        {
            moveDir += gravityDir * _gravity;
        }

        return moveDir;
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
