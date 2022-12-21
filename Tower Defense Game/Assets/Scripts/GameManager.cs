using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject endUI;
    public Text endMessage;
    public static int lives;
    public int start_lives = 30;

    public static GameManager Instance;
    private EnemySpawner enemySpawner;
    
    private void Awake()
    {
        Instance = this;
        lives = start_lives;
        enemySpawner = GetComponent<EnemySpawner>();
    }
    public void Win()
    {
        endUI.SetActive(true);
        endMessage.text = "You win";
    }

    public void Lose()
    {
        enemySpawner.Stop();
        endUI.SetActive(true);
        endMessage.text = "You lose";
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Live_down(){
        lives -= 1;
        if(lives == 0){
            Lose();
        }
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
