namespace Logic.Damage
{
    public class Health : GenericHealth
    {
        private float currentHealth;

        protected override float GetCurrentHealth()
        {
            return currentHealth;
        }

        protected override void SetCurrentHealth(float value)
        {
             currentHealth = value;
        }
    }
}