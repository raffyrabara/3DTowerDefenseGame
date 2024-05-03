using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMngr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject exitPanel;
    public GameObject levelPanel;
    void Start()
    {
        exitPanel.SetActive(false);
        levelPanel.SetActive(false);
    }

    public void PlayGame()
{
   levelPanel.SetActive(true);
}

public void Level1Select()
{
   SceneManager.LoadScene("Level1"); 
}

public void Level2Select()
{
   SceneManager.LoadScene("Level2"); 
}

public void CloseLevelPanel()
{
  levelPanel.SetActive(false);  
}

public void QuitGame()
   {
     exitPanel.SetActive(true);
   }

public void YesSelect()
   {
    Application.Quit();
   }

public void NoSelect()
    {   
        exitPanel.SetActive(false);
    }   
    
}
