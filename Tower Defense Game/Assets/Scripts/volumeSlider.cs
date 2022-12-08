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
    //playerPrefs to save their prefernces
    private void Load()
    {
        //sets volume slider to variable that was saved as musicVolume
        volumeSlide.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlide.value);
    }
}
