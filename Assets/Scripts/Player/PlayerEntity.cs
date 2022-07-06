using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerEntity : MonoBehaviour
{
    [SerializeField] InputActionReference _movement, _lookAround;
    [SerializeField] float _speed = .1f;
    [SerializeField] float _turnSensitivity = .1f;
    CharacterController _controller;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 movementInput = _movement.action.ReadValue<Vector2>();
        Vector3 moveDir = (transform.forward * movementInput.y) + 
        (transform.right * movementInput.x);
        _controller.SimpleMove(moveDir * _speed);

        Vector2 lookAroundInput = _lookAround.action.ReadValue<Vector2>();
        transform.rotation = Quaternion.Euler
        (0, 
        (transform.rotation.eulerAngles.y + lookAroundInput.x * Time.deltaTime * _turnSensitivity), 
        0);
    }
}
