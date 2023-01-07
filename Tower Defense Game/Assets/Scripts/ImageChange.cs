using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    public BuildManager turret;
    public Image image;
    public Color tempColor;
    //public int r = turret.random;
    // Start is called before the first frame update
    void Start()
    {
        turret.random = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(turret.random == 0)
        {
            ColorUtility.TryParseHtmlString("#00FF23", out tempColor);
            image.color = tempColor;
        }
        else if(turret.random == 1)
        {
            ColorUtility.TryParseHtmlString("#FF000B", out tempColor);
            image.color = tempColor;
        }
        else if(turret.random == 2)
        {
            ColorUtility.TryParseHtmlString("#FAFF00", out tempColor);
            image.color = tempColor;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#979797", out tempColor);
            image.color = tempColor;
        }
    }
}
