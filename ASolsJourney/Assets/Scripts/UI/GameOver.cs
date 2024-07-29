using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    GameController gc;
    public void Setup()
    {
        gameObject.SetActive(true);
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void OnClickRestart()
    {
        Debug.Log("Restarting Game");
        gc.Restart();
        gameObject.SetActive(false);
    }

    public void OnClickMainMenu()
    {
        Debug.Log("Going to main menu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -1);
    }
}
