using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_Colissions : MonoBehaviour
{
    private Vector3 backImpulse = -Vector3.forward;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Character_Controller controller = GetComponent<Character_Controller>();

        if (hit.collider.tag == "Wall")
        {
            if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed())
            {
                controller.AddBackImpact(backImpulse);
            }
        }

        if (hit.collider.tag == "Enemy")
        {
            SceneManager.LoadScene("Gameplay");
        }

        if (hit.collider.tag == "Star")
        {
            int newCountStars = Count_Stars.count_stars.GetStarCount() + 1;
            Count_Stars.count_stars.SetStarCount(newCountStars);
            Destroy(hit.gameObject);
            //audio

            if (newCountStars == 6)
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
