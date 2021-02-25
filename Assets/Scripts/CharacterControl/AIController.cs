using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] float dwellTimeAtWaypoint = 2f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] bool shouldMove = true;
        [SerializeField] float attackSpeed = 5f;
        [SerializeField] float patrolSpeed = 3f;

        float distanceToPlayer;
        float timeSinceLastSeenPlayer = Mathf.Infinity;   // timeSinceLastSawPlayer
        float waitTimeAtCurrentWaypoint = 0f;
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
            agent.speed = attackSpeed;
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
            agent.speed = patrolSpeed;
            Vector3 nextPosition = GetCurrentWaypointPosition(currentWaypointIndex);
            mover.StartMovement(nextPosition);  

            if (AtWaypoint())
            {
                WaitThenMoveToNextWaypoint();
            }
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

        void WaitThenMoveToNextWaypoint()
        {
            waitTimeAtCurrentWaypoint += Time.deltaTime;

            if (waitTimeAtCurrentWaypoint > dwellTimeAtWaypoint)
            {
                SetNextWaypoint();
                waitTimeAtCurrentWaypoint = 0f;
            }
        }

        void SetNextWaypoint()
        {
            int nextWaypointIndex = patrolPath.CycleWaypoint(currentWaypointIndex);
            currentWaypointIndex = nextWaypointIndex;
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