using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class liveui : MonoBehaviour
{
    //the text of player's lives
    public TMP_Text livesText;
    
    // Update player's lives once per frame
    void Update()
    {
        livesText.text = GameManager.lives + " Lives";
    }
}
