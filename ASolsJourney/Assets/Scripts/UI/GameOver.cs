using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void OnClickRestart()
    {
        Debug.Log("Restarting Game");
    }

    public void OnClickMainMenu()
    {
        Debug.Log("Going to main menu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -1);
    }
}
