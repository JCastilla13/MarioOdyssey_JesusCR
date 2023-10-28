using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawn_Cappy : MonoBehaviour
{
    [SerializeField]
    private GameObject cappyToSpawn;

    private GameObject cappySpawned;

    private Vector3 forwardPlusPosition = new Vector3(0, 1, 3);

    private Vector3 bouncePlayerDirection = Vector3.up;
   
    [SerializeField]
    private float bouncePlayerForce = 10f;

    [SerializeField]
    private CharacterController characterController;

    private void Update()
    {

        if (cappySpawned == null && Input_Manager._INPUT_MANAGER.GetCappyButton())
        {
            if (cappyToSpawn != null)
            {
                cappySpawned = Instantiate(cappyToSpawn, transform.position + forwardPlusPosition, transform.rotation);
                Destroy(cappySpawned, 8f);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Cappy")
        {
            characterController.Move(bouncePlayerDirection * bouncePlayerForce);
            
        }
    }


}
