using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretMngr : MonoBehaviour
{

    public Transform target;

    [Header("General")]
    public float range = 2.5f;
    public Transform partToRotate;
    public Transform firePoint;
    

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

   // [Header("Use Magic")]
   // public bool useMagic = false;
    
    [Header("Enemy Setup Field")]
    public string enemyTag = "Enemy";
    public float turnSpeed = 15f;

    private Animator towerAnimator;
    private bool isFollowingEnemy = false;

    void Start()
    {
     //   partToRotateAnimator = GetComponent<Animator>();
        towerAnimator = GetComponent<Animator>();
        InvokeRepeating ("UpdateTarget", 0f, 0.5f);
    }


    void UpdateTarget ()
    {
        GameObject [] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
             isFollowingEnemy = true;
        }
        else
        {
            target = null;
            isFollowingEnemy = false;
        }
        towerAnimator.SetBool("isFollowingEnemy", isFollowingEnemy);
    }

    // Update is called once per frame
   void Update()
    {
        if (target == null)
            return;

        if (target != null)
        {
            // If there's a target (enemy in range), follow it
            LockOnTarget();
            
             if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
        } 
    }

    void LockOnTarget ()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Quaternion rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed);
        partToRotate.rotation = rotation;
    }


    void Shoot()
    {
       GameObject bulletGo = (GameObject)Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
       Bullet bullet = bulletGo.GetComponent<Bullet>();

        if (bullet != null)
        bullet.Seek(target);

    }


    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }



}
