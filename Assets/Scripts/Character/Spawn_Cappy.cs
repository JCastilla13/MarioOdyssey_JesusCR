using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawn_Cappy : MonoBehaviour
{
    [SerializeField]
    private GameObject cappyToSpawn;

    private GameObject cappySpawned;

    private Vector3 forwardPlusPosition = new Vector3(0, 0, 3);

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
            //impulso
        }
    }


}
