using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace World.Lighting
{
    public class LightHandler : MonoBehaviour
    {
        public List<WorldLight> lights;

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                EnableLight(1);
            }
        }


        public void EnableLight(int id)
        {
            foreach (WorldLight light in lights)
            {
                if(light.lightID == id)
                {
                    light.ToggleLight();
                }
            }
        }

        public void EnableLight(string lightName)
        {
            foreach (WorldLight light in lights)
            {
                if (light.lightName == lightName)
                {
                    light.ToggleLight();
                }
            }
        }
    }
}