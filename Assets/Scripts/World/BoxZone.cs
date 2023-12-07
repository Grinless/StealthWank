using Helpers;
using UnityEngine;

[System.Serializable]
public class BoxZone
{
    public float width; 
    public float height;
    public Color gizmoColor;

    public Vector2 Scale => new Vector2(width, height);

    public bool InZone(Vector3 zonePos, Vector3 otherPos)
    {
        return Helper_CollisionDetection.AABB(zonePos, Scale, otherPos);
    }
}