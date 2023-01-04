using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCamera : MonoBehaviour
{
    public Transform bannerLookTarget;
    //banner text component
    public Canvas hpSlider;
    // Start is called before the first frame update
    void Start()
    {
        bannerLookTarget = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.transform.rotation = Quaternion.LookRotation(hpSlider.transform.position - bannerLookTarget.position);
    }
}
