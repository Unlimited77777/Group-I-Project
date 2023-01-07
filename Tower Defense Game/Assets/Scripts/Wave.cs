using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Save the attributes needed for each wave of enemy generation
[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public int count;
    public float rate;
    public int reward;

}
