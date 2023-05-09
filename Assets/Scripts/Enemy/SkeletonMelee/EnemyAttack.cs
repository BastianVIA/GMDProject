using Player;
using UnityEngine;

namespace Enemy.SkeletonMelee
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private AudioSource attackAudio;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float attackRange = 1.5f;
        [SerializeField] private float attackCooldown = 2f;

        private Animator animator;
        private Transform player;
        private bool isAttacking;
        private float lastAttackTime;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (player == null) return;
            
            if (animator.GetBool("isDead"))
            {
                isAttacking = false;
                return;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (CanAttack(distanceToPlayer))
            {
                isAttacking = true;
                lastAttackTime = Time.time;

                if (PlayerHealth.Instance != null)
                {
                    PlayAttackAudio();
                    Invoke(nameof(Hit), 0.5f);
                }
            }
            else
            {
                isAttacking = false;
            }
            animator.SetBool("isAttacking", isAttacking);
        }

        private bool CanAttack(float distanceToPlayer)
        {
            return distanceToPlayer <= attackRange && Time.time - lastAttackTime > attackCooldown;
        }

        private void PlayAttackAudio()
        {
            attackAudio.Play();
        }

        private void Hit()
        {
            PlayerHealth.Instance.TakeDamage(damage);
        }
    }
}