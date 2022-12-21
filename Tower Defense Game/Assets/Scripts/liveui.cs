using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class liveui : MonoBehaviour
{
    public TMP_Text livesText;
    // Start is called before the first frame update    
    
    // Update is called once per frame
    void Update()
    {
        livesText.text = GameManager.lives + " Lives";
    }
}
