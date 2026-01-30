using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    private InputAction move;
    private InputAction look;

    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float gravity = -30.0f;

    private Vector3 velocity;

    [SerializeField] private float rotationSpeed = 4.0f;
    [SerializeField] private float mouseSensY = 1.0f;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;

    private float cameraPitch;

    private void OnValidate()
    {
        this.ValidateRefs();
    }

    void Start()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        look = InputSystem.actions.FindAction("Player/Look");
    }

    void Update()
    {
        Vector2 readMove = move.ReadValue<Vector2>();
        Vector2 readLook = look.ReadValue<Vector2>();

        // Player Movement
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;
        velocity.y += gravity * Time.deltaTime;
        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;
        controller.Move(movement);

        // Player Rotation
        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * Time.deltaTime);

        // Camera Rotation
        cameraPitch -= readLook.y * mouseSensY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }
}