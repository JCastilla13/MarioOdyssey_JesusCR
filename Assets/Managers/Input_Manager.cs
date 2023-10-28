using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Input_Manager : MonoBehaviour
{
    private PlayerInputActions playerInputs;
    public static Input_Manager _INPUT_MANAGER;

    private float timeSinceJumpPressed = 0f;

    private Vector2 leftAxisValue = Vector2.zero;

    private Vector2 mouseAxisValue = Vector2.zero;

    private float cappyButtonPressed = 0f;

    private float crouchButtonPressed = 0f;

    private float backJumpButtonPressed = 0f;



    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            playerInputs = new PlayerInputActions();
            playerInputs.Character.Enable();
            playerInputs.Character.Jump.performed += JumpButtonPressed;
            playerInputs.Character.Move.performed += LeftAxisUpdate;
            playerInputs.Character.Camera.performed += MouseAxisUpdate;
            playerInputs.Character.SpawnCappy.performed += CappyButton;
            playerInputs.Character.Crouch.performed += CrouchButton;
            playerInputs.Character.BackJump.performed += BackJumpButton;
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }

    }
    private void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;

        cappyButtonPressed += Time.deltaTime;

        crouchButtonPressed += Time.deltaTime;

        backJumpButtonPressed += Time.deltaTime;

        InputSystem.Update();
    }
    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        timeSinceJumpPressed = 0f;
    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();
    }

    private void MouseAxisUpdate(InputAction.CallbackContext context)
    {
        mouseAxisValue = context.ReadValue<Vector2>();
    }

    private void CappyButton(InputAction.CallbackContext context)
    {
        cappyButtonPressed = 0f;
    }

    private void CrouchButton(InputAction.CallbackContext context)
    {
        crouchButtonPressed = 0f;
    }

    private void BackJumpButton(InputAction.CallbackContext context)
    {
        backJumpButtonPressed = 0f;
    }

    public bool GetSouthButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }

    public Vector2 GetLeftAxisUpdate()
    {
        return this.leftAxisValue; 
    }

    public Vector2 GetMouseAxisUpdate()
    {
        return this.mouseAxisValue;
    }

    public bool GetCappyButton()
    {
        return this.cappyButtonPressed == 0f;
    }

    public bool GetCrouchButton()
    {
        return this.crouchButtonPressed == 0f;
    }

    public bool GetBackJumpButton()
    {
        return this.backJumpButtonPressed == 0f;
    }
}
