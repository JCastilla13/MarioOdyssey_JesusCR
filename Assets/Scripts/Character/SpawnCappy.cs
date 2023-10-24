using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnCappy : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn; 

    [SerializeField]
    private float destroyDelay = 5f; 

    private bool buttonsPressed = true;

    [SerializeField]
    private Vector3 vc = new Vector3(0, 0, 3);

    [SerializeField]
    private Vector3 imapct = new Vector3(0, 1, 0);

    [SerializeField]
    private CharacterController characterController;

    private void Update()
    {
        if (Gamepad.current != null &&
            Gamepad.current.buttonNorth.isPressed || Keyboard.current.fKey.isPressed)
        {
            if (buttonsPressed)
            {
                buttonsPressed = false;
                Capy();
            }
        }
        else
        {
            buttonsPressed = true;
        }

    }

    private void Capy()
    {
        if (objectToSpawn != null)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn, transform.position + vc, transform.rotation);
            Destroy(spawnedObject, destroyDelay);
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Capy")
        {
            //characterController.SimpleMove(Vector3.up * 2000f * Time.deltaTime);
        }
    }


}
