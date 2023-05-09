using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private int damage = 5;
        [SerializeField] private AttackArea attackArea;
        [SerializeField] private AudioSource swordHitSound;
        [SerializeField] private AudioSource swordMissSound;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (!context.started || IsAttacking()) return;

            animator.SetTrigger("MeleeAttack");

            RemoveDeadEnemiesFromDamageables();

            if (attackArea.damageablesInAttackArea.Count > 0)
            {
                swordHitSound.PlayDelayed(0.5f);
            }
            else
            {
                swordMissSound.PlayDelayed(0.2f);
            }

            Invoke(nameof(Hit), 0.5f);
        }

        private void RemoveDeadEnemiesFromDamageables()
        {
            attackArea.damageablesInAttackArea.RemoveAll(damageable => damageable.IsDead());
        }

        private bool IsAttacking()
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_OneHanded");
        }

        private void Hit()
        {
            foreach (var damageable in attackArea.damageablesInAttackArea)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}