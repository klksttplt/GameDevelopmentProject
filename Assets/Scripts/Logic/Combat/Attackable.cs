using Logic.Common;

namespace Logic.Combat
{
    public class Attackable : ContextComponent
    {
        // Public API

        public void TakeDamage(Damage takenDamage)
        {
            Health.TakeDamage(takenDamage);
        }
    }
}
