﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using RPG.Control;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] bool shouldMove = true;

        float distanceToPlayer;
        float timeSinceLastSeenPlayer = Mathf.Infinity;   // timeSinceLastSawPlayer
        private int currentWaypointIndex = 0;

        Fighter fighter;
        CharacterMovement mover;
        GameObject player;
        ActionScheduler actionScheduler;
        NavMeshAgent agent;

        private void Start()
        {
            string objectName = gameObject.name;    // delete?

            if (patrolPath == null)
            { Debug.LogError("Patrol path is Null " + objectName); }

            fighter = GetComponent<Fighter>();
            mover = GetComponent<CharacterMovement>();
            player = GameObject.FindWithTag("Player");
            actionScheduler = GetComponent<ActionScheduler>();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (this.shouldMove) { FindDistanceToPlayerAndAttack(); }
        }

        private void FindDistanceToPlayerAndAttack()
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= chaseDistance)
            {
                AttackState();
            }
            else if (distanceToPlayer > chaseDistance)
            {
                if (timeSinceLastSeenPlayer <= suspicionTime)
                {
                    WaitInSuspenseState();
                }
                else if (timeSinceLastSeenPlayer > suspicionTime)
                {
                    PatrolState();                // PatrolBehavior
                }
            }

            timeSinceLastSeenPlayer += Time.deltaTime;
        }

        void AttackState()
        {
            actionScheduler.CancelCurrentAction();
            fighter.Attack(player);
            timeSinceLastSeenPlayer = 0;
        }

        private void WaitInSuspenseState()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void PatrolState()
        {
            actionScheduler.CancelCurrentAction();

            Vector3 nextPosition = GetCurrentWaypointPosition(currentWaypointIndex);
            mover.StartMovement(nextPosition);  

            if (AtWaypoint())
            {
                int nextWaypointIndex = patrolPath.CycleWaypoint(currentWaypointIndex);
                currentWaypointIndex = nextWaypointIndex;
            }

            print(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            if (!agent.pathPending && agent != null)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        private Vector3 GetCurrentWaypointPosition(int wayPointIndex)
        {
            return patrolPath.GetWaypointTransform(wayPointIndex).position;
        }

        // called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}