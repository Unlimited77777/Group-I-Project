using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPause : MonoBehaviour
{
    //the ButtonPauseMenu
    public GameObject ingameMenu;
    public void OnPause(){
        Time.timeScale = 0;
        ingameMenu.SetActive(true);
    }
    public void OnResume(){
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
    }
    public void OnRestart(string sceneName){
        //Loading Scene0
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }
}
