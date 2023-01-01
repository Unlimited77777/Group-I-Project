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
    public MapCube[] list;
    public Enemy[] enemyList;
    public BuildManager moneySystem;
    public GameManager lives;

    public void OnPause(){
        Time.timeScale = 0;
        ingameMenu.SetActive(true);
    }
    public void OnResume(){
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
    }
    public void OnRestart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        Save save = new Save();
        Scene scene = SceneManager.GetActiveScene();
        save.map = scene.name;
        //save.money = moneySystem.money;
        //save.lives = lives.live;
        //foreach(Enemy enemy in enemyList)
        //{
        //    save.enemy.hp = enemy.hp;
        //    save.enemy.speed = enemy.speed;
        //}
        //foreach (MapCube cube in list)
        //{
        //    if (cube != null)
        //    {
        //      save.turret.Add(cube);
        //      save.tpositionX = Cubemap.positonX;
        //      save.tpositionY = Cubemap.positonY;
        //      save.tpositionZ = Cubemap.positonZ;
        //    }
        //}


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
            moneySystem.money = save.money;
            // for(int i = 0; i< save.turret.Count; i++)
            // {
            //    MapCube newTurret = save.turret[i];
            //    newTurret.BuildTurret(newTurret.turretData);
            // }
        }
        else
        {
            //REPORT THE ERROR
            Debug.Log("NOT FOUND");
        }
    }
}
