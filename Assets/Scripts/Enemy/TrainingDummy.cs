using System;
using Interface;
using UnityEngine;

namespace Enemy
{
    public class TrainingDummy : MonoBehaviour, IDamageable
    {
        public int currentHealth;
        public int maxHealth;
        public HealthBar healthBar;
        private Animator animator;

        private void Awake()
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            animator = GetComponent<Animator>();
        }
        public void TakeDamage(int damageAmount)
        {
            animator.SetTrigger("IsAttacked");
            currentHealth -= damageAmount;
            healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                transform.Translate(new Vector3(0, -20, 0));
                Destroy(gameObject, 1);
            }
        }

        public bool IsDead()
        {
            throw new NotImplementedException();
        }

        private void OnDestroy()
        {
            Debug.Log("Enemy has died");
        }
    }
}