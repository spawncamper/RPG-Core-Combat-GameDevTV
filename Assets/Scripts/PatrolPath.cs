using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float waypointGizmoRadius = 0.5f;

        int waypointIndex;

        private void OnDrawGizmos()
        {
            for (int wayPointIndex = 0; wayPointIndex < transform.childCount; wayPointIndex++)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(GetWaypointTransform(wayPointIndex).position, waypointGizmoRadius);

                if (wayPointIndex <= (transform.childCount - 2))
                { Gizmos.DrawLine(GetWaypointTransform(wayPointIndex + 1).position, GetWaypointTransform(wayPointIndex).position); }
                else if (wayPointIndex == (transform.childCount - 1))
                {
                    { Gizmos.DrawLine(GetWaypointTransform(wayPointIndex).position, GetWaypointTransform(0).position); }
                }
            }
        }

        public Transform GetWaypointTransform(int wayPointIndex)
        {
            return transform.GetChild(wayPointIndex).transform;
        }

        public int GetChildCount()
        {
            return transform.childCount;
        }

        public int CycleWaypoint(int waypointIndex)   //  this needs to go into the PatrolPath.cs
        {
            if (waypointIndex <= (GetChildCount() - 2))
            {
                waypointIndex++;
            }
            else if (waypointIndex == (GetChildCount() - 1))
            {
                waypointIndex = 0;
            }
            return waypointIndex;
        }
    }
}