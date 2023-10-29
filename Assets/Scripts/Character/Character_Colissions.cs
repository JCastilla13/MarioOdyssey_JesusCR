using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_Colissions : MonoBehaviour
{
    private Vector3 backImpulse = -Vector3.forward;
    private Vector3 bouncePlayerDirection = Vector3.up;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Character_Controller controller = GetComponent<Character_Controller>();

        if (hit.collider.tag == "Cappy")
        {
            controller.AddCappyImpulse(bouncePlayerDirection);
        }

        if (hit.collider.tag == "Wall")
        {
            if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed())
            {
                controller.AddWallImpact(backImpulse);
            }
        }

        if (hit.collider.tag == "Enemy")
        {
            SceneManager.LoadScene("Gameplay");
        }

        if (hit.collider.tag == "Moon")
        {
            int newCountMoons = Count_Moons.count_moons.GetStarCount() + 1;
            Count_Moons.count_moons.SetStarCount(newCountMoons);
            Destroy(hit.gameObject);
            //audio

            if (newCountMoons == 13)
            {
                SceneManager.LoadScene("GameWin");
            }
        }

        if(hit.collider.tag == "Platform")
        {
            controller.AddPlatformImpulse(backImpulse);
        }
    }
}
