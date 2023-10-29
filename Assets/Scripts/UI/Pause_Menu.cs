using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    private bool isPaused;

    [SerializeField] GameObject menuObject;
    [SerializeField] GameObject playerController;
    [SerializeField] GameObject cameraController;

    private void Start()
    {
        isPaused = false;
        menuObject.SetActive(false);
    }

    void Update()
    {
        Character_Controller controller = playerController.GetComponent<Character_Controller>();
        Gameplay_Camera gameplay_camera = cameraController.GetComponent<Gameplay_Camera>();

        if (Input_Manager._INPUT_MANAGER.GetPauseMenuButton())
        {
            if (!isPaused)
            {
                controller.enabled = false;
                gameplay_camera.enabled = false;
                menuObject.SetActive(true);
                isPaused = true;
            }
            else
            {
                controller.enabled = true;
                gameplay_camera.enabled = true;
                menuObject.SetActive(false);
                isPaused = false;
            }
        }
    }

    public void Resume()
    {
        Character_Controller controller = playerController.GetComponent<Character_Controller>();
        Gameplay_Camera gameplay_camera = cameraController.GetComponent<Gameplay_Camera>();

        controller.enabled = true;
        gameplay_camera.enabled = true;
        menuObject.SetActive(false);
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
