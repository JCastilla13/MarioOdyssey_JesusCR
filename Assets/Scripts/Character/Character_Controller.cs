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
    private int countTypeJump = 1;

    [SerializeField]
    private GameObject cam;

    //[SerializeField]
    //private InputActionReference inputActionRef;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        countTimeToJump += Time.deltaTime;

        //Calcular dirección XZ
        Vector3 direction = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f) * new Vector3(0f, 0f, 0f);
        direction.Normalize();

        //Calcular velocidad XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;

        //Asignar dirección Y
        direction.y = -1f;

        //Calcular gravedad
        if (controller.isGrounded)
        {
            if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed() && countTypeJump == 1)
            {
                finalVelocity.y = jumpForce;
                countTypeJump = 2;
                countTimeToJump = 0f;
            }

            else if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed() && countTypeJump == 2)
            {
                if (countTimeToJump <= 5f)
                {
                    finalVelocity.y = jumpForce * 2;
                    countTypeJump = 3;
                    countTimeToJump = 0f;
                }
            }

            else if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed() && countTypeJump == 3)
            {
                if (countTimeToJump <= 5f)
                {
                    finalVelocity.y = jumpForce * 3;
                    countTypeJump = 1;
                    countTimeToJump = 0f;
                }
            }
        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
            coyoteTime -= Time.deltaTime;
        }

        if(countTimeToJump >= 5f)
        {
            countTypeJump = 1;
        }

        if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed() && coyoteTime >= 0f)
        {
            finalVelocity.y = jumpForce;
            coyoteTime = 0f;
        }

        controller.Move(finalVelocity * Time.deltaTime);
        Debug.Log(countTimeToJump);
    }

}
;