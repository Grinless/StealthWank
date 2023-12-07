using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR;

namespace World.Lighting
{
    public class WorldLight : MonoBehaviour
    {
        public Light2D Light;
        public string lightName;
        public int lightID;
        public bool lightActive;
        public BoxZone boxZone;

        public void Start()
        {
            if (Light == null)
                Light = GetComponent<Light2D>();

            Light.enabled = lightActive;

            lightName = gameObject.name;
        }

        public void ToggleLight()
        {
            lightActive = !lightActive;
            Light.enabled = lightActive;
        }

        private void OnDrawGizmos()
        {
            Helper_Gizmos.DrawWireCube(boxZone.gizmoColor, transform.position, boxZone.Scale);
        }
    }
}