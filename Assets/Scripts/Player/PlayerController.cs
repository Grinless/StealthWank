using Helpers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

#region Structs. 
[System.Serializable]
public struct VibrationKey
{
    public KeyCode key;
    public VibrationState state;
    public float audioRadius; 
    public float pleasureRate;
    public float vibrationRate;

    public bool CheckInput()
    {
        return Input.GetKeyUp(this.key);
    }
}

[System.Serializable]
public struct VibrationControl
{
    public VibrationKey key_None;
    public VibrationKey key_Low;
    public VibrationKey key_Medium;
    public VibrationKey key_High;

    public VibrationKey? GetCurrentState()
    {
        if (key_None.CheckInput())
            return key_None;

        if (Input.GetKeyUp(key_Low.key))
            return key_Low;

        if (Input.GetKeyUp(key_Medium.key))
            return key_Medium;

        if (Input.GetKeyUp(key_High.key))
            return key_High;

        return null;
    }
}

[System.Serializable]
public enum VibrationState
{
    NONE = 0,
    LOW = 3,
    MEDIUM = 5,
    HIGH = 15
}
#endregion

[System.Serializable]
public struct Range
{
    public float min;
    public float max;

    public Range(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}

public class PlayerController : MonoBehaviour
{
    private const float LL_INNER_RADIUS = 0.5f;
    private readonly Vector3 FACING_DIRECTION = new Vector3(0, 1, 0).normalized;
    private static PlayerController instance; 
    private Light2D lustLight;
    private GameObject vibrationRingIndicator;
    public Transform playerTransform;
    public VibrationControl control;
    public VibrationKey currentKey;
    public Range lustLightRange;
    public GameObject pleasureRingPrefab;
    public GameObject vibrationIndicatorPrefab; 
    public float currentPleasureValue = 0;
    
    public static PlayerController Instance => instance;
    public static float CurrentVibrationValue => instance.currentPleasureValue;
    private float VibrationRadius => currentKey.audioRadius;
    private float PleasureRate => currentKey.pleasureRate;
    private float VibrationRate => currentKey.vibrationRate;

    void Start()
    {
        currentKey = control.key_None; 
        instance = this;
        lustLight = GameObject.Instantiate(pleasureRingPrefab, playerTransform.position, playerTransform.rotation).GetComponent<Light2D>();
        vibrationRingIndicator = GameObject.Instantiate(vibrationIndicatorPrefab, playerTransform.position, playerTransform.rotation);
    }

    void Update()
    {
        //Update the current vibration state. 
        UpdateVibrationState();
        //Update the amount of pleasure gained this frame. 
        currentPleasureValue += PleasureRate * Time.deltaTime;
        //Update the device output. 
        ServerHandler.Instance.SetIntensity(VibrationRate);
        //Update the lust light. 
        AdjustLight();
        AdjustRing(); 
    }

    public bool CheckFacing(Vector3 direction)
    {
        Vector3 dirNorm = direction.normalized;
        float detectedCross = Vector3.Dot(dirNorm, FACING_DIRECTION);

        if (detectedCross > 1 && detectedCross < -1)
            return true;

        return false;
    }

    #region Vibration Ring.
    
    public void AdjustRing()
    {
        vibrationRingIndicator.transform.localScale = 
            2 * new Vector3(VibrationRadius, VibrationRadius); 
    }

    #endregion

    #region Lust Lighting.
    public void AdjustLight()
    {
        float value = Mathf.Clamp(
            (currentPleasureValue * 10),
            lustLightRange.min,
            lustLightRange.max
            ); 

        lustLight.pointLightInnerRadius = value - LL_INNER_RADIUS;
        lustLight.pointLightOuterRadius = value;
    }
    #endregion

    #region Vibration Control. 
    private void UpdateVibrationState()
    {

        //Get the updated state. 
        var s = control.GetCurrentState();

        //If the updated state is not null, assign it. 
        if (s != null)
            currentKey = (VibrationKey)s;
    }

    #endregion

    #region Collision Detection.
    public bool CheckIfInsideRadius(Transform transform)
    {
        return Helper_CollisionDetection.CheckIfInsideRadius(
            transform,
            playerTransform,
            VibrationRadius
            );
    }

    #endregion

    #region Gizmos.

    private void OnDrawGizmos()
    {
        Helpers.Helper_Gizmos.DrawRadius(Color.red, playerTransform, VibrationRadius);
    }

    #endregion
}
