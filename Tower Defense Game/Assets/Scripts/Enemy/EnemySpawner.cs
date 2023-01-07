using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int EnemyAlive = 0;
    public Wave[] waves;
    public Transform START;
    public float waveRate = 0.2f;
    private Coroutine coroutine;
    public Waypoints waypoint;
    private BuildManager moneyManager;
    void Start()
    {
        coroutine = StartCoroutine(SpawnEnemy());
        moneyManager = GameObject.Find("wave").GetComponent<BuildManager>();
    }
    public void Stop()
    {
        StopCoroutine(coroutine);
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(waveRate);
        foreach(Wave wave in waves)
        {
            for(int i = 0; i < wave.count; i++)
            {
                GameObject enemy = GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().waypoint = waypoint;
                EnemyAlive++;
                if(i != wave.count-1)
                    yield return new WaitForSeconds(wave.rate);
            }
            while(EnemyAlive > 0)
            {
                yield return 0;
            }
            moneyManager.UpdateMoney(wave.reward);
            yield return new WaitForSeconds(waveRate);
        }
        while (EnemyAlive > 0)
        {
            yield return 0;
        }
        GameManager.Instance.Win();
    }
}
