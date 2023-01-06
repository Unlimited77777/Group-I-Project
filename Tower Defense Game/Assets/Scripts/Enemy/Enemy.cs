using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float speed = 10;
    public int hp = 150;
    private int totalHp;
    private Slider hpSlider;
    private Transform[] positions;
    private int index = 0;
    public int reward = 20;
    public Waypoints waypoint;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        positions = waypoint.positions;
        totalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (index > positions.Length - 1) return;
        transform.LookAt(positions[index].position);
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed, Space.World);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        if(index > positions.Length - 1)
        {
            ReachDestination();
        }
    }

    //arrive at the end point
    void ReachDestination()
    {
        GameManager.Instance.Live_down();
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        EnemySpawner.EnemyAlive--;
    }

    public void TakeDamage(int damage)
    {
        //get the damage when being attacked from turret and show the details of hp by a slider
        if (hp <= 0) return;
        hp -= damage;
        hpSlider.value = (float)hp / totalHp;

        //die when hp<0
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(this.gameObject);
    }
    
}
