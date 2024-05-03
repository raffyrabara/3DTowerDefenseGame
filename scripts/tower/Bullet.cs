using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    public float speed = 50f;

    public float explosionRadius = 0f;

    public int damage = 50;

    public GameObject impactEffect;
    // Start is called before the first frame updat
    public void Seek (Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate (dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }
    void HitTarget ()
    {
         // Instantiate the impactEffect GameObject

    GameObject effectIns = Instantiate(impactEffect, transform.position, Quaternion.Euler(-90, 0, 0));
   // Destroy the impactEffect after 2 seconds 
    Destroy(effectIns, 1f);
    // Destroy the bullet object
    if (explosionRadius > 0f)
    {
        Explode ();
    }
    else
    {
        Damage(target);
        DamageBoss(target);
    }

    Destroy(gameObject);
 
    }

    void Explode ()
    {
       Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
       foreach (Collider collider in colliders)
       {
        if (collider.tag == "Enemy")
        {
            //  Destroy(gameObject);
           Damage(collider.transform);
           DamageBoss(collider.transform);
           
        }
        
        
       }
    }

    void Damage (Transform enemy)
    {
        Enemy1 e = enemy.GetComponent<Enemy1> ();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
        
    }

    void DamageBoss (Transform enemy)
    {
        EnemyBoss eB = enemy.GetComponent<EnemyBoss> ();
        if (eB != null)
        {
            eB.TakeDamage(damage);
        }
        
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
