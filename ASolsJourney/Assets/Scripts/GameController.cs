using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameOverScreen gameOverScreen;
    private PlayerController pc;
    private Player player;
    [SerializeField] private GridSpawner gridSpawner;
    bool gameOver;

    public void Start()
    {
        gameOver = false;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }
    public void GameOver()
    {
        gameOver = true;
        gameOverScreen.Setup();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void Restart()
    {
        gameOver = false;
        pc.RevivePlayer();
        player.Heal(player.MaxHp);
    }
}
