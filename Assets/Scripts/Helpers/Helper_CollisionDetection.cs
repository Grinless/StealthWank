using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public static class Helper_CollisionDetection
    {
        public static bool CheckIfInsideRadius(Transform transA, Transform transB, float radius)
        {
            //Cache positions. 
            Vector2 circleCenter = transB.position;
            Vector2 otherCenter = transA.position;
            Vector2 radiusVec = radius * circleCenter.normalized;
            //Determine the vector length between the two points. 
            Vector2 distVec = circleCenter - otherCenter;

            return (distVec.magnitude < radiusVec.magnitude);
        }

        public static bool AABB(Vector3 boxPosition, Vector3 boxSize, Vector3 otherPos)
        {
            bool collisionX = 
                (otherPos.x <= boxPosition.x + boxSize.x && 
                 otherPos.x >= boxPosition.x - boxSize.x);
            bool collisionY =
                (otherPos.y <= boxPosition.y + boxSize.y && 
                otherPos.y >= boxPosition.y - boxSize.y);

            //Calculate AABB
            return (collisionX && collisionY);
        }
    }
}
