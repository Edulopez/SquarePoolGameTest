using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class PositionHelper : MonoBehaviour
    {

        /// <summary>
        /// Rotate object around another
        /// </summary>
        public static void RotateAround(float horizontalMovement, GameObject center, GameObject obj)
        {
            var angle = horizontalMovement * 30 * Time.deltaTime;

            obj.transform.RotateAround(center.transform.position, Vector3.up, angle);
            obj.transform.LookAt(center.transform);
        }
    }
}
