using Interface;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy.SkeletonMelee
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth = 20f;
        [SerializeField] private float currentHealth;
        [SerializeField] private Slider healthBar;
        [SerializeField] private AudioSource dieAudio;
        
        private Animator animator;
        private DamageTextSpawner damageTextSpawner;
        private IItemDropper itemDropper;
        

        private void Awake()
        {
            animator = GetComponent<Animator>();
            damageTextSpawner = GetComponent<DamageTextSpawner>();
            itemDropper = GetComponent<ItemDropper>();
            currentHealth = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        public void TakeDamage(int damageAmount)
        {
           if (IsDead())
           {
               return;
           }
           currentHealth -= damageAmount;
           healthBar.value = currentHealth;
            
           damageTextSpawner.SpawnDamageText(damageAmount, transform);
            
           if (currentHealth <= 0)
           {
               Die();
           }
        }

        private void Die()
        {
            transform.Translate(new Vector3(0, -20, 0));
            animator.SetBool("isDead", true);
            dieAudio.Play();
            healthBar.gameObject.SetActive(false);
            itemDropper.DropItem();
            Destroy(gameObject, 10);
        }

        public bool IsDead()
        {
            return this == null || animator.GetBool("isDead");
        }
    }
}
