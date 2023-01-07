using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 50;
    private Transform target;
    public GameObject impactEffect;
    public float speed = 70f;
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        //destory the bullet if there is no enemy in the range of turret
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        
        //attack enmey when enemy is in the range
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    //attack the enemy
    void HitTarget()
    {
        //Get Enemy Component
        Enemy enemy = target.GetComponent<Enemy>();
        
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        enemy.TakeDamage(damage);
        Destroy(gameObject);

    }
}
