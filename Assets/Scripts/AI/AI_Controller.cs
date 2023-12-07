using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public struct Need
{
    public string needName;
    public int identifier;
    public float currentValue;
    public float degredationValue;
    public float expectedMinimum;

    public void DegradeValue(float dt)
    {
        currentValue -= degredationValue * dt;
    }

    public float RequiredDegree()
    {
        if (currentValue < expectedMinimum)
            return currentValue;
        return 0f;
    }
}

[System.Serializable]
public struct AIValues
{
    public List<Need> needs;

    public void DegradeNeeds(float dt)
    {
        foreach (Need need in needs)
        {
            need.DegradeValue(dt);
        }
    }

    public Need CheckGreatestNeed()
    {
        int id = 0;
        float needValue = needs[0].RequiredDegree();

        for (int i = 1; i < needs.Count; i++)
        {
            if (needs[i].RequiredDegree() < needValue)
            {
                id = i;
                needValue = needs[i].RequiredDegree();
            }
        }

        return needs[id];
    }
}

public class AI_Controller : MonoBehaviour
{
    public List<ZoneMarker> markers = new List<ZoneMarker>();
    public AIValues values;
    public Need? currentNeed = null;
    public ZoneNeedProvider? provider = null; 

    public Vector3 Position
    {
        get { return transform.position; } 
        set { transform.position = value; }
    }

    public void Update()
    {
        //Update the current state of the needs. 
        values.DegradeNeeds(Time.deltaTime);

        //Update the currently selected need here. 
        if(provider != null)
        {
            Need n = ((Need)currentNeed);
            ZoneNeedProvider p = ((ZoneNeedProvider)provider);
            n.currentValue += p.provisionTick * Time.deltaTime;
        }

        //Add some movement logic here. 


        //Add zone entering logic here. 

        //Add vibration detection event logic. 

        //Add masturbation detection event here. 
    }

    private void FixedUpdate()
    {
        //Update the selected need. 
        if(GetNextNeed())
            currentNeed = values.CheckGreatestNeed();
    }

    private bool GetNextNeed()
    {
        if (currentNeed == null ||
            (currentNeed != null && ((Need)currentNeed).RequiredDegree() >= 1))
            return true; 
        return false;
    }
}
