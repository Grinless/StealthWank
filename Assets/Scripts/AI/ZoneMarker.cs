using Helpers;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ZoneNeedProvider
{
    public string needName;
    public int needIdentifier;
    public float provisionTick; 
}

[System.Serializable]
public struct ZoneValues
{
    public List<ZoneNeedProvider> providers;
}

public struct ZoneData
{
    public int ID;
    public string name; 
    public ZoneValues values;
    
    public bool ContainsID(int id)
    {
        foreach (ZoneNeedProvider provider in values.providers)
        {
            if(provider.needIdentifier == id)
                return true;
        }

        return false;
    }
}

public class ZoneMarker : MonoBehaviour
{
    public BoxZone zone; 

    public void OnDrawGizmos()
    {
        Helper_Gizmos.DrawWireCube(zone.gizmoColor, transform.position, zone.Scale);
    }

    public void OnGUI()
    {
        Vector3 worldPos = transform.localToWorldMatrix * gameObject.transform.position;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        Rect labelRect = new Rect(screenPos, new Vector3(100, 100));
        GUI.Label(labelRect, "This is a test");
    }
}
