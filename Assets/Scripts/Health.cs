using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;
        Animator animator;
        CapsuleCollider targetCollider;
        NavMeshAgent agent;

        private void Start()
        {
            animator = GetComponent<Animator>();
            targetCollider = GetComponent<CapsuleCollider>();
            agent = GetComponent<NavMeshAgent>();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints <= 0 && isDead == false)
            {
                Die();
            }

            print(healthPoints);
        }

        private void Die()
        {
            animator.SetTrigger("die");
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
            targetCollider.enabled = !targetCollider.enabled; // turn off Target altogether?
            agent.enabled = !agent.enabled;
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}