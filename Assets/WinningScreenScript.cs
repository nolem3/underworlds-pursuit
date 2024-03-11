using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningScreenScript : MonoBehaviour
{
     public void Setup()
    {
        gameObject.SetActive(true);
        // Pause the game
        Time.timeScale = 0;
    }

    public void NextLevelButton()
    {
        //resume the game
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
        
}
