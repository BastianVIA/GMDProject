using UI;
using UI.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public static PlayerHealth Instance;
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private Slider healthBar;
        [SerializeField] private DamageTextSpawner damageTextSpawner;

        private Animator animator;
        private float currentHealth;

        private void Start()
        {
            Instance = this;
            animator = GetComponent<Animator>();
            currentHealth = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            healthBar.value = currentHealth;
            damageTextSpawner.SpawnDamageText((int)damage, transform);
            if (currentHealth <= 0f)
            {
                Die();
            }
        }

        public void Heal()
        {
            currentHealth += 25;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthBar.value = currentHealth;
        }

        private void Die()
        {
            animator.SetBool("IsDead", true);
            Destroy(gameObject, 5f);
        }
    }
}