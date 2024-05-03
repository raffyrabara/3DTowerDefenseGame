using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarMngr : MonoBehaviour
{
    public EnemyBoss enemyBoss;
    public Image fillImage;
    private Slider slider;
    private CanvasGroup canvasGroup;

    public GameObject faceHealth;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
     // Make the health bar face the camera, but maintain its own rotation
        Vector3 lookAtPoint = Camera.main.transform.position;
        lookAtPoint.y = faceHealth.transform.position.y; // Keep the same Y level
        faceHealth.transform.LookAt(lookAtPoint);
        faceHealth.transform.rotation = Quaternion.Euler(60f, 0f, 0f);
        float fillValue = enemyBoss.health / enemyBoss.maxhealth;

        if (!enemyBoss.isTakingDamage)
        {
            // Hide the health bar when the enemy is dead
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            // Show the health bar when the enemy is alive and taking damage
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }

        if (fillValue <= slider.maxValue / 3)
        {
            fillImage.color = Color.red;
        }
        else if (fillValue > slider.maxValue / 3)
        {
            fillImage.color = Color.green;
        }
        slider.value = fillValue;
    }
}
