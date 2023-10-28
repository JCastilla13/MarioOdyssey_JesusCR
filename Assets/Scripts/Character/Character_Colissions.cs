using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_Colissions : MonoBehaviour
{

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Wall")
        {
            if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed())
            {
                //codigo empuje hacia atras
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
        }
    }
}
