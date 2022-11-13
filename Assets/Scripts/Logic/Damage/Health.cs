using UnityEngine;

namespace Logic.Damage
{
    [DefaultExecutionOrder(-19)] // before components that use Health but after Stats
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