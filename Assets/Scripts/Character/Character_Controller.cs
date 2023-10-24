using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Controller : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 finalVelocity = Vector3.zero;
    private float velocityXZ = 5f;
    private float gravity = 20f;
    private float jumpForce = 8f;
    private float coyoteTime = 1f;

    private float countTimeToJump;

    [SerializeField]
    private float firstJumpForce = 8;

    [SerializeField]
    private float jumpForceIncrement = 3;

    [SerializeField]
    private int countTypeJump = 1;

    [SerializeField]
    private int countMaxTypeJump = 3;

    [SerializeField]
    private GameObject cam;

    private Vector3 inputKeysMovement = Vector3.zero;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        countTimeToJump += Time.deltaTime;

        //Calcular dirección XZ
        Vector3 direction = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f) * new Vector3(inputKeysMovement.x, 0f, inputKeysMovement.z);
        direction.Normalize();

        Vector2 inputVector = Input_Manager._INPUT_MANAGER.GetLeftAxisUpdate();
        inputKeysMovement = new Vector3(inputVector.x, 0f, inputVector.y);
        inputKeysMovement.Normalize();

        //Calcular velocidad XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;

        //Asignar dirección Y
        direction.y = -1f;

        //Calcular gravedad
        if (controller.isGrounded)
        {
            coyoteTime = 0.1f;

            if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed())
            {
                
                countTimeToJump = 0f;
                finalVelocity.y = firstJumpForce;
                firstJumpForce += jumpForceIncrement;
                countTypeJump++;

                if (countTypeJump > countMaxTypeJump)
                {
                    firstJumpForce = 8f; 
                    countTypeJump = 1;
                }
            }
        }

        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
            coyoteTime -= Time.deltaTime;
        }

        if (countTimeToJump >= 3f)
        {
            firstJumpForce = 8f;
            countTypeJump = 1;
            countTimeToJump = 0f;
        }

        controller.Move(finalVelocity * Time.deltaTime);
    }
}


