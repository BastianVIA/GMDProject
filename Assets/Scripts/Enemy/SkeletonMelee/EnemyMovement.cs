using UnityEngine;
using UnityEngine.AI;

namespace Enemy.SkeletonMelee
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform player;
        [SerializeField] private float obstacleAvoidanceRange = 2f;
        [SerializeField] private float followRange = 10f;
        [SerializeField] private float stoppingRange = 3f;
        private CapsuleCollider collider;
        public Animator animator;
        private Rigidbody rigidbody;

        private NavMeshAgent navAgent;
        private bool isMoving;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            navAgent = GetComponent<NavMeshAgent>();
            collider = GetComponent<CapsuleCollider>();
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (player == null)
            {
                return;
            }

            if (animator.GetBool("isDead"))
            {
                navAgent.isStopped = true;
                collider.enabled = false;
                rigidbody.constraints = RigidbodyConstraints.FreezePosition;
                return;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= stoppingRange)
            {
                navAgent.isStopped = true;
                animator.SetBool("isFighting", true);
            }
            else
            {
                animator.SetBool("isFighting", false);
                if (distanceToPlayer <= followRange)
                {
                    navAgent.SetDestination(player.position);
                    AvoidObstacle();
                }
                else
                {
                    navAgent.isStopped = true;
                    isMoving = false;
                }

                animator.SetBool("isMoving", isMoving);
            }
        }

        private void AvoidObstacle()
        {
            RaycastHit hit;
            bool obstacleInFront = Physics.Raycast(transform.position, transform.forward, out hit,
                obstacleAvoidanceRange);

            if (obstacleInFront)
            {
                Vector3 avoidanceDirection = Vector3.Reflect(transform.forward, hit.normal);
                navAgent.Move(avoidanceDirection * navAgent.speed * Time.deltaTime);

                isMoving = false;
            }
            else
            {
                navAgent.SetDestination(player.position);
                navAgent.isStopped = false;
                isMoving = true;
            }
        }
    }
}