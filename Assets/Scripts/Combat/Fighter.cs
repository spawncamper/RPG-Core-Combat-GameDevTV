using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        // serialized field
        [SerializeField] float weaponRange = 5.0f;
        [SerializeField] float timeBetweenAttacks = 1.0f;
        [SerializeField] float attackDamage = 5f;            // weaponDamage

        // private variables
        float distanceToTarget;
        float timeSinceLastAttack = Mathf.Infinity;

        // components
        Transform target;
        CharacterMovement characterMovement;
        ActionScheduler actionScheduler;
        Animator animator;
        Health health;

        private void Start()
        {
            characterMovement = GetComponent<CharacterMovement>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (target != null && !health.IsDead())
           {
                GetInRangeAndAttack();
            } 
        }

        private void GetInRangeAndAttack()
        {
            distanceToTarget = Vector3.Distance(transform.position, target.position);  // GetInRange()

            if (distanceToTarget > weaponRange)                                        // GetInRange()
            {
                characterMovement.MoveToDestination(target.position);     // CharacterMovement.cs = Mover.cs, MoveToDestination() = MoveTo()
            }
            else if (distanceToTarget <= weaponRange)
            {
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            characterMovement.Cancel();                  // TriggerAttack()
                             
            transform.LookAt(target.transform);

            if (timeSinceLastAttack >= timeBetweenAttacks && !target.GetComponent<Health>().IsDead())
            {
                timeSinceLastAttack = 0f;
                animator.ResetTrigger("stopAttack");
                animator.SetTrigger("attack");    // TriggerAttack()
            }
        }

        public void Attack(GameObject currentTarget) // currentTarget = combatTarget,  // CanAttack()
        {
            actionScheduler.StartAction(this);
            target = currentTarget.transform;
        }

        public void Cancel()
        {
            //animator.ResetTrigger("Attack");
            animator.SetTrigger("stopAttack");
            target = null;
            GetComponent<CharacterMovement>().Cancel();
        }

        // Animation Event, in Unarmed attack animation, called within Animator
        void Hit()
        {
            if (target != null)
                target.GetComponent<Health>().TakeDamage(attackDamage);
        }
    }
}