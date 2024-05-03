using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
   
    // Update is called once per frame
    private bool isgameEnded = false;

    public GameObject gameOverUI;
    public GameObject gameWin;

    void Update (){
    if (isgameEnded)
    {
        return;
    }
    if (PlayerStats.Lives <=0)
    {
        EndGame();
    }
    }


    void EndGame ()
{
    PlayerStats.Lives = 0;
    isgameEnded = true;
    gameOverUI.SetActive(true);

    // Use Invoke to call a method after a delay (2 seconds)
    Invoke("SetTimeScaleToZero", 2.0f);
}

// Method to set Time.timeScale to 0f after 2 seconds
void SetTimeScaleToZero()
{
    Time.timeScale = 0f;
}

    public void RestartGO()
    {
        SceneManager.LoadScene("Level1");
      //  Time.timeScale = 1f;
    }


    public void RestartGOLvl2()
    {
        SceneManager.LoadScene("Level2");
      //  Time.timeScale = 1f;
    }
    
    public void BackToMenuGo()
    {
        SceneManager.LoadScene("MainMenu");
      //  Time.timeScale = 1f;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level2");
    }

public void GameWon()
{
    isgameEnded = true;
    gameWin.SetActive(true);
}
}
