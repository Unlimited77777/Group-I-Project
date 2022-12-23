using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class ButtonPause : MonoBehaviour
{
    //the ButtonPauseMenu
    public GameObject ingameMenu;
    public string LoadMap;
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

    public void SaveButton()
    {
        SaveGame();
    }

    public void LoadButton()
    {
        LoadGame();
    }



    public void SaveGame()
    {
        //save the game
        Save save = new Save();
        Scene scene = SceneManager.GetActiveScene();
        save.map = scene.name;
        BinaryFormatter bf = new BinaryFormatter();

        FileStream fileStream = File.Create(Application.persistentDataPath + "/Data.text");

        bf.Serialize(fileStream, save);

        fileStream.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/Data.text"))
        {
            //LOAD THE GAME
            BinaryFormatter bf = new BinaryFormatter();

            FileStream fileStream = File.Open(Application.persistentDataPath + "/Data.text", FileMode.Open);

            Save save = bf.Deserialize(fileStream) as Save;//You have loaded your previous "save" object
            fileStream.Close();

            LoadMap = save.map;
            SceneManager.LoadScene(LoadMap);
            Time.timeScale = 1f;
        }
        else
        {
            //REPORT THE ERROR
            Debug.Log("NOT FOUND");
        }
    }
}
