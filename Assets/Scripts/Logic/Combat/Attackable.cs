using Logic.Common;

namespace Logic.Combat
{
    public class Attackable : ContextComponent
    {
        // Public API

        public void TakeDamage(Damage takenDamage)
        {
            if (Health.IsDead)
                return;
            
            Health.TakeDamage(takenDamage);
            if (Health.IsAlive)
            {
                Animable.TakeDamage();
                Animable.Attack(true);
                AttackController.isHit = true;
            }
        }
        
    }
}
