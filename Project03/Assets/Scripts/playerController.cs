using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(fPSInput))]
[RequireComponent(typeof(fPSMotor))]
public class playerController : MonoBehaviour
{
    fPSInput _input = null;
    fPSMotor _motor = null;

    [SerializeField] float _moveSpeed = .1f;
    [SerializeField] float _turnSpeed = 6f;
    [SerializeField] float _jumpStrength = 10f;
    [SerializeField] float _sprintStrength = 2f;
    [SerializeField] float _teleportCheck = 12f;
    [SerializeField] float distance = 8f;
    private int voice = 0;



    [SerializeField] Camera cameraController = null;

    [SerializeField] Transform rayStart = null;

    [SerializeField] AudioSource TeleportSound = null;
    [SerializeField] AudioSource TracerVoice1 = null;
    [SerializeField] AudioSource TracerVoice2 = null;

    [SerializeField] GameObject defaultWeapon = null;
    [SerializeField] GameObject player = null;

    private void Awake()
    {
        _input = GetComponent<fPSInput>();
        _motor = GetComponent<fPSMotor>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _input.MoveInput += OnMove;
        _input.RotateInput += OnRotate;
        _input.JumpInput += OnJump;
        _input.TeleportInput += OnTeleport;
    }

    private void OnDisable()
    {
        _input.MoveInput -= OnMove;
        _input.RotateInput -= OnRotate;
        _input.JumpInput -= OnJump;
        _input.TeleportInput -= OnTeleport;
    }

    void OnMove(Vector3 movement)
    {
        _motor.Move(movement * _moveSpeed);
    }

    void OnRotate(Vector3 rotation)
    {
        _motor.Turn(rotation.y * _turnSpeed);
        _motor.Look(rotation.x * _turnSpeed);
    }

    void OnJump()
    {
        _motor.Jump(_jumpStrength);
    }

    void OnTeleport()
    {
        RaycastHit hit;
        Vector3 destination = cameraController.transform.position + cameraController.transform.forward * distance;
        TeleportSound.Play();
        voice++;

        if (voice == 3)
        {
            TracerVoice1.Play();
        }
        if (voice == 6)
        {
            TracerVoice2.Play();
        }

        if (Physics.Linecast(cameraController.transform.position, destination, out hit))
        {
            destination = cameraController.transform.position + cameraController.transform.forward * (hit.distance - 1f);
        }

        if (Physics.Raycast(destination, -Vector3.up, out hit))
        {
            destination = hit.point;
            destination.y = 0.5f;
            transform.position = destination;
        }

    }
}
