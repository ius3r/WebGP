using KBCore.Refs;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    private InputAction move;
    private InputAction look;
    private InputAction jump;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float gravity = -30.0f;
    private Vector3 velocity;
    [SerializeField] private float rotationSpeed = 4.0f;
    [SerializeField] private float mouseSensY = 5.0f;
    private float camXRotation;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;
    [SerializeField, Scene] private AudioController audioController;
    private void OnValidate()
    {
        this.ValidateRefs();
    }

    void Start()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        look = InputSystem.actions.FindAction("Player/Look");
        jump = InputSystem.actions.FindAction("Player/Jump");
        jump.started += Jump;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        jump.started -= Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        audioController.PlayJumpSFX();
    }

    void Update()
    {
        Vector2 readMove = move.ReadValue<Vector2>();
        Vector2 readLook = look.ReadValue<Vector2>();// (0,0)
        // Movement of the player
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;
        velocity.y += gravity * Time.deltaTime;
        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;
        controller.Move(movement);

        // Rotation of the player
        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * Time.deltaTime);

        // Rotate the Camera
        camXRotation += mouseSensY * readLook.y * Time.deltaTime * -1;
        camXRotation = Mathf.Clamp(camXRotation, -90f, 90f);
        cam.gameObject.transform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

    public void ChangeMouseSensibility(float value)
	{
		Debug.Log($"Value changed - {value}");
		mouseSensY = value;
		rotationSpeed = value;
	}
}