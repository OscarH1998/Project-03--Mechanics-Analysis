using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class fPSInput : MonoBehaviour
{
    [SerializeField] bool _invertVertical = false;

    public event Action<Vector3> MoveInput = delegate { };
    public event Action<Vector3> RotateInput = delegate { };
    public event Action JumpInput = delegate { };
    public event Action TeleportInput = delegate { };

    public float cooldownTime = 2;
    private float nextFireTime = 0;

    void Update()
    {
        DetectMoveInput();
        DetectRotateInput();
        DetectJumpInput();
        DetectTeleportInput();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void DetectMoveInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if (xInput != 0 || yInput != 0)
        {
            Vector3 _horizontalMovement = transform.right * xInput;
            Vector3 _forwardMovement = transform.forward * yInput;

            Vector3 movement = (_horizontalMovement + _forwardMovement).normalized;

            MoveInput?.Invoke(movement);
        }
    }

    void DetectRotateInput()
    {
        float xInput = Input.GetAxisRaw("Mouse X");
        float yInput = Input.GetAxisRaw("Mouse Y");

        if (xInput != 0 || yInput != 0)
        {
            if (_invertVertical == true)
            {
                yInput = -yInput;
            }

            Vector3 rotation = new Vector3(yInput, xInput, 0);

            RotateInput?.Invoke(rotation);
        }
    }

    void DetectJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpInput?.Invoke();
        }
    }

    void DetectTeleportInput()
    {
        if (Time.time > nextFireTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TeleportInput?.Invoke();
                nextFireTime = Time.time + cooldownTime;
            }
        }
    }
}
