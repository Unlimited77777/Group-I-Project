using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class liveui : MonoBehaviour
{
    public Text livesText;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        livesText.text = GameManager.lives.ToString() + " Lives";
    }
}
