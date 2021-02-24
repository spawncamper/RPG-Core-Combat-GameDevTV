using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class CharacterMovement : MonoBehaviour, IAction   // Move
    {
        private NavMeshAgent agent;
        private Animator animator;
        private ActionScheduler scheduler;

        void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            animator = gameObject.GetComponent<Animator>();
            scheduler = gameObject.GetComponent<ActionScheduler>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMovement(Vector3 destination)      // StartMoveAction()
        {
            scheduler.StartAction(this);
            MoveToDestination(destination);
        }

        public void MoveToDestination(Vector3 destination)
        {
            if (agent.enabled)
            {
                agent.SetDestination(destination);
                agent.isStopped = false;
            }
        }

        public void Cancel()
        {
            if (agent.enabled)
            { agent.isStopped = true; }
        }

        private void UpdateAnimator()
        {
            //       Debug.Log(agent.velocity.magnitude);
            // ForwardSpeed is the Animator parameter, 
            animator.SetFloat("ForwardSpeed", agent.velocity.magnitude);
        }
    }
}

