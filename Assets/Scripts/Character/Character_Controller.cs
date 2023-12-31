using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Controller : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 finalVelocity = Vector3.zero;
    private float velocityXZ = 5f;
    private float gravity = 20f;

    private float countTimeToJump;

    private int countTypeJump = 1;
    private int countMaxTypeJump = 3;

    private float firstJumpForce = 10;
    private float jumpForceAdded = 4;

    private int backJumpForce = 10;
    private int backJumpForceUp = 15;

    private int frontJumpForce = 15;
    private int frontJumpForceUp = 15;

    private int backWallImpact = 14;
    private int backWallImpactUp = 16;

    private int addCappyImpulse = 20;

    private int addPlatformImpulse = 25;

    [SerializeField] private GameObject cam;

    private bool isCrouching = false;

    private Vector3 inputKeysMovement = Vector3.zero;
    private Vector3 lastMoveDirection = Vector3.forward;

    private Animator anim;

    private float currVelocityAnimation = 0;
    private float incrementVelocityTimeAnimation = 8f;
    private float acceleration = 6f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        countTimeToJump += Time.deltaTime;

        //Calcular direcci�n XZ
        Vector3 direction = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f) * new Vector3(inputKeysMovement.x, 0f, inputKeysMovement.z);
        transform.rotation = Quaternion.LookRotation(direction);
        direction.Normalize();

        Vector2 inputVector = Input_Manager._INPUT_MANAGER.GetLeftAxisUpdate();
        inputKeysMovement = new Vector3(inputVector.x, 0f, inputVector.y);
        inputKeysMovement.Normalize();

        //Calcular velocidad XZ
        finalVelocity.x = Mathf.MoveTowards(finalVelocity.x, direction.x * velocityXZ, acceleration * Time.deltaTime);
        finalVelocity.z = Mathf.MoveTowards(finalVelocity.z, direction.z * velocityXZ, acceleration * Time.deltaTime);

        if (inputKeysMovement.magnitude > 0)
        {
            currVelocityAnimation += incrementVelocityTimeAnimation * Time.deltaTime;

            currVelocityAnimation = Mathf.Min(currVelocityAnimation, 5);

            lastMoveDirection = direction;
        }

        else
        {
            currVelocityAnimation = 0;

            transform.rotation = Quaternion.LookRotation(lastMoveDirection);
        }

        anim.SetFloat("velocity", currVelocityAnimation);

        //Asignar direcci�n Y
        direction.y = -1f;

        if (controller.isGrounded)
        {
            if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed())
            {
                if (!isCrouching)
                {
                    anim.SetBool("isGrounded", false);

                    countTimeToJump = 0f;

                    anim.SetInteger("jumpType", countTypeJump);

                    finalVelocity.y = firstJumpForce;
                    firstJumpForce += jumpForceAdded;
                    countTypeJump++;

                    if (countTypeJump > countMaxTypeJump)
                    {
                        firstJumpForce = 10f;
                        countTypeJump = 1;
                    }
                }
                else
                {
                    anim.SetBool("isGrounded", false);
                    countTimeToJump = 0f;
                    finalVelocity.y = 5;
                }
            }

            else
            {
                anim.SetBool("isGrounded", true);
            }
        }

        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
        }

        if (finalVelocity.y < -0.1f)
        {
            anim.SetBool("isFalling", true);
        }

        else
        {
            anim.SetBool("isFalling", false);
        }

        if (countTimeToJump >= 4f)
        {
            firstJumpForce = 10f;
            countTypeJump = 1;
            countTimeToJump = 0f;
        }

        if (Input_Manager._INPUT_MANAGER.GetCrouchButton())
        {
            if (!isCrouching)
            {
                isCrouching = true;
                anim.SetBool("isCrouch", true);
                controller.height = 0.75f;
                velocityXZ = 2f;
            }
            else
            {
                isCrouching = false;
                anim.SetBool("isCrouch", false);
                controller.height = 2.0f;
                velocityXZ = 5.0f;
            }
        }

        if (controller.isGrounded && isCrouching)
        {
            if (Input_Manager._INPUT_MANAGER.GetBackJumpButton())
            {
                finalVelocity += -transform.forward * backJumpForce;
                finalVelocity.y = backJumpForceUp;
            }

            if (Input_Manager._INPUT_MANAGER.GetFrontJumpButton())
            {
                finalVelocity += transform.forward * frontJumpForce;
                finalVelocity.y = frontJumpForceUp;
            }
        }


        controller.Move(finalVelocity * Time.deltaTime);
    }

    public void AddCappyImpulse(Vector3 direction)
    {
        finalVelocity.y = addCappyImpulse;
    }

    public void AddPlatformImpulse(Vector3 direction)
    {
        finalVelocity.y = addPlatformImpulse;
    }

    public void AddWallImpact(Vector3 direction)
    {
        if (!controller.isGrounded)
        {
            finalVelocity += -transform.forward * backWallImpact;
            finalVelocity.y = backWallImpactUp;
            lastMoveDirection = -lastMoveDirection;
        }
    }
}


