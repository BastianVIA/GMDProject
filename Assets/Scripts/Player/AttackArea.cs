using System;
using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace Player
{
    public class AttackArea : MonoBehaviour
    {
        public List<IDamageable> damageablesInAttackArea { get; } = new();

        public void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageablesInAttackArea.Add(damageable);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null && damageablesInAttackArea.Contains(damageable))
            {
                damageablesInAttackArea.Remove(damageable);
            }
        }
    }
}
