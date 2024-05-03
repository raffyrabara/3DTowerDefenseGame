using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
   public float speed = 1f;
   private Transform target;
   private int wavepointIndex = 0;
   public float rotationSpeed = 5.0f;

   public int moneyGain = 50;
   //private int enemyCounter = 6;

   public float health;
   public float maxhealth = 3000f;

    private Animator anim;
    private bool isDead = false;
    public bool isTakingDamage = false;

    private bool isMagicEffectActive = false;
    private float magicEffectTimer;
    private float originalSpeed;
    


void Start ()
{
    anim = GetComponent<Animator>();
    target = Waypoints.points[0];
    health = maxhealth;
}

public void TakeDamage (int amount)
{
    if(isDead)
    return;

    isTakingDamage = true;

    health -= amount;
    Debug.Log(health);

    if (health <= 0)
    {
     //   isDead = true;
        Die();
    }
}

void Die ()
{
    //PlayerStats.Money += moneyGain;
    isDead = true;
    anim.SetBool("isDead", isDead);
    StartCoroutine(DestroyAfterAnimation());
    WaveSpawner.EnemiesAlive--;

}
IEnumerator DestroyAfterAnimation()
{
    // Wait for the length of the "Die" animation
    yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);

    PlayerStats.Money += moneyGain;

 
    Destroy(gameObject);
}

public void ApplyMagicEffect(float duration, float slowFactor, float damagePerSecond)
{
    if (isMagicEffectActive)
    {
        // If the magic effect is already active, reset the timer
        magicEffectTimer = duration;
    }
    else
    {
        // Apply the magic effect
        isMagicEffectActive = true;
        magicEffectTimer = duration;
        originalSpeed = speed;
        speed = speed - (speed * slowFactor);
        
        // Start taking continuous damage
        StartCoroutine(TakeContinuousDamage(damagePerSecond));
    }
}

private IEnumerator TakeContinuousDamage(float damagePerSecond)
{
    while (isMagicEffectActive)
    {
        TakeDamage(Mathf.RoundToInt(damagePerSecond * Time.deltaTime));
        yield return new WaitForSeconds(1.0f);
    }
}



void Update ()
{
    if (isDead)
    return;

if (isMagicEffectActive)
    {
        // Reduce the magic effect timer and check if it has expired
        magicEffectTimer -= Time.deltaTime;
        if (magicEffectTimer <= 0f)
        {
            // Restore the original speed and stop taking continuous damage
            isMagicEffectActive = false;
            speed = originalSpeed;
        }
    }
Debug.Log("Actual Speed: " + speed);


    Vector3 dir = target.position - transform.position;
    transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

 // Calculate the rotation to look at the next waypoint
    Quaternion lookRotation = Quaternion.LookRotation(dir);
    // Smoothly rotate the enemy towards the waypoint direction
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    
    if (Vector3.Distance(transform.position, target.position) <= 0.4f)
    {
        GetNextWaypoint();
    }
}

void GetNextWaypoint()
{
    if (wavepointIndex >= Waypoints.points.Length - 1)
    {
        EndPath();
        return;
    }

    wavepointIndex++;
    target = Waypoints.points[wavepointIndex];
    
}
void EndPath ()
{
    PlayerStats.Lives = PlayerStats.Lives - 10;
    WaveSpawner.EnemiesAlive--;
    Destroy(gameObject);
}
}

