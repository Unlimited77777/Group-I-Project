using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class TimelineNextScene : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] PlayableDirector currentDirector = null;
    [SerializeField] private float newTime;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(NextScene());
    }

    public void skipCutScene()
    {

        currentDirector.time = newTime;
        //currentDirector.Play();
        //SceneManager.LoadScene("Map1-easy");
    }

    public void loadLevel()
    {
        SceneManager.LoadScene(nextScene);
    }

}
