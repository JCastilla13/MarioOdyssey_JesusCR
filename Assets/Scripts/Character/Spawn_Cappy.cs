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
    private CharacterController characterController;

    private void Update()
    {

        if (cappySpawned == null && Input_Manager._INPUT_MANAGER.GetCappyButton())
        {
            if (cappyToSpawn != null)
            {
                Vector3 cappySpawnPosition = characterController.transform.position + characterController.transform.forward * forwardPlusPosition.z;
                cappySpawnPosition.y += forwardPlusPosition.y;

                cappySpawned = Instantiate(cappyToSpawn, cappySpawnPosition, transform.rotation);
                Destroy(cappySpawned, 6f);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Cappy")
        {
            Character_Controller controller = GetComponent<Character_Controller>();
            controller.AddCappyImpulse(bouncePlayerDirection);
        }
    }


}
