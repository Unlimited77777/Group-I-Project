using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    [SerializeField][TextArea] private string[] storyInfo;
    [SerializeField] private float textSpeed = 0.01f;

    [SerializeField] private TextMeshProUGUI storyText;
    private int currentDisplayingText = 0;

    private void Start()
    {
        StartCoroutine(AnimateText());
    }

    public void ActivateText()
    {
        //if they press continue button - coroutine
        StartCoroutine(AnimateText());
    
    }

    IEnumerator AnimateText()
    {
       
        for (int i = 0; i < storyInfo[currentDisplayingText].Length+1; i++)
        {
            storyText.text = storyInfo[currentDisplayingText].Substring(0, i);

            yield return new WaitForSeconds(textSpeed);
        }
        if (currentDisplayingText+1 < storyInfo.Length) {
            currentDisplayingText = currentDisplayingText + 1;
        }
    }
}
