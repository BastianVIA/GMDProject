namespace Interface
{
    public interface IDamageable
    {
        public void TakeDamage(int damageAmount);
        public bool IsDead();
    }
}
