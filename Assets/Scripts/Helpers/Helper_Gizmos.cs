using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Helpers
{
    public static class Helper_Gizmos
    {
        public static void DrawWireCube(UnityEngine.Color color, Vector2 position, Vector2 size)
        {
            Gizmos.color = color;
            Gizmos.DrawWireCube(position, size);
        }

        public static void DrawRadius(
            UnityEngine.Color color, UnityEngine.Transform t, float radius)
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(t.position, radius);
            Gizmos.DrawLine(t.position, (Vector2)t.position + new Vector2(radius, radius));
        }
    }
}
