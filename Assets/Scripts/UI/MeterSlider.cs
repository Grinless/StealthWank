using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterSlider : MonoBehaviour
{
    public Image fillImage;
    float min = 0.0F; 
    float max = 1.0F;
    public float currentFill; 

    public void Start()
    {
        currentFill = min;
        UpdateFill(); 
    }

    public void Update()
    {
        UpdateFill(); 
    }

    public void UpdateFill()
    {
        Vector3 scaleVec = transform.localScale;
        scaleVec.y = Mathf.Clamp(currentFill, min, max);
        fillImage.transform.localScale = scaleVec;
    }
}
