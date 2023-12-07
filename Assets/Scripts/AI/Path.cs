using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [System.Serializable]
    public struct Waypoint
    {
        public GameObject point;

        public Vector2 Position => point.transform.position; 

        public float GetDistance(Vector2 pos)
        {
            Vector2 p1 = Position; 
            return (p1 - pos).magnitude;
        }
    }

    public class Path : MonoBehaviour
    {
        public List<Waypoint> waypoints;


        private void OnDrawGizmos()
        {
            for(int i = 0; i < waypoints.Count - 1; i++)
            {
                Gizmos.DrawLine(waypoints[i].Position, waypoints[i + 1].Position);
            }
        }
    }
}