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
    private BuildManager moneyManager;
    private Transform[] positions;
    private int index = 0;
    public int reward = 20;
    // Start is called before the first frame update
    void Start()
    {
        positions = Waypoints.positions;
        totalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();
        moneyManager = GameObject.Find("wave").GetComponent<BuildManager>();
    }

    // Update is called once per frame
    void Update()
    {
       Move(); 
    }

    void Move()
    {
        if(index > positions.Length - 1) return;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if(Vector3.Distance(positions[index].position, transform.position) < 0.2f)
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
        GameManager.Instance.Lose();
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        EnemySpawner.EnemyAlive--;
    }

    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        hpSlider.value = (float)hp / totalHp;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
        moneyManager.UpdateMoney(reward);
    }
}
