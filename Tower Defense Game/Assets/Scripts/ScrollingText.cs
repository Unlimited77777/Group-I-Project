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
        //so text will automatically start showing
        StartCoroutine(AnimateText());
    }

    public void ActivateText()
    {
        //if they press continue button - coroutine
        StartCoroutine(AnimateText());
    
    }

    IEnumerator AnimateText()
    {
        //for word in storyInfo
        //currentDisplayingText is used as index for storyInfo because it has 3 elements
        for (int i = 0; i < storyInfo[currentDisplayingText].Length+1; i++)
        {
            //storyText is an empty text box and sets the text to a substring up to the current word i for every 0.01s
            storyText.text = storyInfo[currentDisplayingText].Substring(0, i);

            yield return new WaitForSeconds(textSpeed);
        }
        if (currentDisplayingText+1 < storyInfo.Length) {
            //when all the words for the current element has been displayed
            //this will increase it to the next element inside of storyInfo
            currentDisplayingText = currentDisplayingText + 1;
        }
    }
}
