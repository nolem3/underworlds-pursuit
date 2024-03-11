using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameOverScript gameOverScreen;
    public WinningScreenScript winningScreen;

    public AHealth playerHealth;
    public AHealth bossHealth;

    public void GameOver()
    {
        gameOverScreen.Setup();
    }
    private void nextLevel()
    {
        winningScreen.Setup();
    }

    public void FixedUpdate()
    {
        if (playerHealth.GetHealth() <= 0)
        {
            GameOver();
        }
        else if (bossHealth.GetHealth() <= 0)
        {
             nextLevel();
        }
    }
}
