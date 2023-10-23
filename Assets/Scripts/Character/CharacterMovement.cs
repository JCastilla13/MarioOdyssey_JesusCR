using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private void Update()
    {
        if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed())
        {
            Debug.Log("Presionado");
        };
    }

}
