using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelmPanel : MonoBehaviour
{
    public ThrottleGroup throttleGroup;

    public TMP_Text portPercentage;
    public TMP_Text starboardPercentage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        portPercentage.text = Mathf.Round(throttleGroup.LeftPercentage * 100).ToString() + " %";
        starboardPercentage.text = Mathf.Round(throttleGroup.RightPercentage * 100).ToString() + " %";
    }
}
