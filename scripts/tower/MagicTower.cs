using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTower : MonoBehaviour
{
    public float magicEffectDuration = 3.0f; // How long the magic effect lasts
    public float range; // The range of the magic tower
    public float slowFactorPercentage = 50f; // The factor by which the enemy's speed is reduced
    public float damagePerSecond = 100f; // The continuous damage per second

    public GameObject rangeIndicator;


    //public GameObject scaledObject; 


    
    private void Update()
    {
        // Find all enemies in range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        
        float slowFactor = slowFactorPercentage / 100.0f;

        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the object is an enemy
            Enemy1 enemy = hitCollider.GetComponent<Enemy1>();
            
            if (enemy != null)
            {
                // Apply the magic effect to the enemy
                enemy.ApplyMagicEffect(magicEffectDuration, slowFactor, damagePerSecond);
                
            }
            EnemyBoss enemyBoss = hitCollider.GetComponent<EnemyBoss>();
            if (enemyBoss != null)
            {
                enemyBoss.ApplyMagicEffect(magicEffectDuration, slowFactor, damagePerSecond);
            }
        }
    }

   public void showRange()
    {
        rangeIndicator.SetActive(true);
    }

    public void hideRange()
    {
        rangeIndicator.SetActive(false);
    }

       void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
