using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void M_StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void M_Quit()
    {
        Application.Quit();
    }

    public void M_ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
