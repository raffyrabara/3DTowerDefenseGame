using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMngr : MonoBehaviour
{
 
 public GameObject pauseMenu;
 public GameObject helpMenu;
 //public GameObject Instruction1;
 //public GameObject Instruction2;
 public GameObject restartConfirm;
 public GameObject backToMainConfirm;
 public GameObject LevelSelectButton;
 public GameObject prepTime;
 //public GameObject previousButton;
 //public GameObject nextButton;
 public static bool isPaused;

void Start()
{
    pauseMenu.SetActive(false);
    helpMenu.SetActive(false);
  //  Instruction1.SetActive(false);
  //  Instruction2.SetActive(false);
    restartConfirm.SetActive(false);
    backToMainConfirm.SetActive(false);
    LevelSelectButton.SetActive(false);
   // previousButton.SetActive(false);
   // nextButton.SetActive(false);
}



public void PauseGame()
    {
        pauseMenu.SetActive(true);
        prepTime.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
 
public void RestartGameConfi()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        restartConfirm.SetActive(true);
    }

public void YesRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
        isPaused = false;
        restartConfirm.SetActive(false);
    }

    public void YesRestartLvl2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
        isPaused = false;
        restartConfirm.SetActive(false);
    }

public void NoRestart()
    {
        pauseMenu.SetActive(true);
        restartConfirm.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }  

public void HelpButton()
    {
       helpMenu.SetActive(true);
      //  Instruction1.SetActive(true);
      //  previousButton.SetActive(true);
      //  nextButton.SetActive(true);
      //  Instruction2.SetActive(false);
      //  Time.timeScale = 0f;
      //  isPaused = true;
    }


public void BackMainConfirm()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        backToMainConfirm.SetActive(true);
    }

public void YesToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
       // backToMainConfirm.SetActive(false);
    }
public void NoToMainMenu()
    {
        pauseMenu.SetActive(true);
        backToMainConfirm.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

public void LevelButtonSelect()
{
    LevelSelectButton.SetActive(true);
}

public void Level1Select()
{
    SceneManager.LoadScene("Level1");
}

public void Level2Select()
{
    SceneManager.LoadScene("Level2");
}

public void CloseSelectLevel()
{
    LevelSelectButton.SetActive(false);
}

public void CloseHelp()
{
    helpMenu.SetActive(false);
}

}
