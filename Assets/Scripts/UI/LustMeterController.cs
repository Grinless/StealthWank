using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LustMeterController : MonoBehaviour
{
    public MeterSlider slider;

    public void Update()
    {
        if (PlayerController.Instance != null)
        {
            //Get dynamic update based on the player controller and display. 
            slider.currentFill = PlayerController.CurrentVibrationValue;

        }
    }
}
