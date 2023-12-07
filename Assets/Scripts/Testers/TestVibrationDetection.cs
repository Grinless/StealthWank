using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVibrationDetection : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        bool inCircle = PlayerController.Instance.CheckIfInsideRadius(transform);
        if(inCircle)
        {
            Debug.Log("Within vibration zone");
        }
    }
}
