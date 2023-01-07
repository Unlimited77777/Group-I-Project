using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ChangeScene : MonoBehaviour
{

    //change the scene
    public void OnStartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
