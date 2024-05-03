using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGamePlay : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject fastButton;
    public GameObject normalButton;
   void Start()
   {
    normalButton.SetActive(false);
    Time.timeScale = 1f;
   }
   public void fastClick()
   {
    normalButton.SetActive(true);
    fastButton.SetActive(false);
    Time.timeScale = 2f;
   }

   public void normalClick()
   {
    normalButton.SetActive(false);
    fastButton.SetActive(true);
    Time.timeScale = 1f;
   }
}
