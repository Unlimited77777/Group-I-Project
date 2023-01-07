using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasCamera : MonoBehaviour
{

    public Transform bannerLookTarget;
    //banner text component
    public TMP_Text tmp_text_banner;
    // Start is called before the first frame update
    void Start()
    {
        bannerLookTarget = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        tmp_text_banner.transform.rotation = Quaternion.LookRotation(tmp_text_banner.transform.position - bannerLookTarget.position);
    }
}
