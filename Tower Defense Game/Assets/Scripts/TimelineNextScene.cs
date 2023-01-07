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

    public void skipCutScene()
    {
        //if you press skip button, it will just skip to the time before the signal is emitted
        currentDirector.time = newTime;
    }

    public void loadLevel()
    {
        //this just loads the next scene after the tutorial ends
        SceneManager.LoadScene(nextScene);
    }

}
