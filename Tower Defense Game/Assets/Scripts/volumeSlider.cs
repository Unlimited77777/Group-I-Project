using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlide;
    [SerializeField] Toggle myToggle;

    // Start is called before the first frame update
    void Start()
    {
        //playerPrefs stores data between game sessions
        //if theres no volume set, then  it'll set the slider to 1
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

    }

    //get the change of volume
    //user can only change the volume if music isn't muted
    public void OnChangeVolume()
    {
        if (myToggle.isOn == true)
        {
            AudioListener.volume = volumeSlide.value;
            Save();
        }
    }

    public void muteMusic()
    {
        if (AudioListener.volume != 0)
        {
            //if music already playing, turns volume off and toggle
            AudioListener.volume = 0;
            myToggle.isOn = false;
        }
        else
        {
            //if they turn music back on, it will set the volume to the slider value
            OnChangeVolume();
        }
    }
    
    private void Load()
    {
        //sets volume slider to variable that was saved as musicVolume
        volumeSlide.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save()
    {
        //saves player prefs as current volume slider value
        PlayerPrefs.SetFloat("musicVolume", volumeSlide.value);
    }
}
