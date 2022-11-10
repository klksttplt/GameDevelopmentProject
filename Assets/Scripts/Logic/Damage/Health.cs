using Logic.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Logic.Damage
{
    public class Health : BaseHealth
    {
        // Events
     
        public override UnityEvent<float> OnHeal { get; } = new UnityEvent<float>();
        
        public override UnityEvent<Combat.Damage> OnDamageTaken { get; } = new UnityEvent<Combat.Damage>();

        public override UnityEvent OnDied { get; } = new UnityEvent();
        
        // Fields: Editor
        
        //Public API
        
        public override float CurrentValue { get; }
        public override float MaxValue { get; set; }
        public override bool IsDead { get; }
        public override StatDef MaxHealthStatDef { get; }
        public override float ChangeHealth(float health)
        {
            throw new System.NotImplementedException();
        }

        public override void TakeDamage(Combat.Damage damageObj, bool ignoreInvulnerability = false)
        {
            throw new System.NotImplementedException();
        }

        public override void Heal(float health)
        {
            throw new System.NotImplementedException();
        }

        public override void Kill(bool ignoreInvulnerability = true)
        {
            throw new System.NotImplementedException();
        }
    }
}
