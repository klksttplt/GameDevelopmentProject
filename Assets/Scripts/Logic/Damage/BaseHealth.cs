using System;
using Logic.Common;
using Logic.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Logic.Damage
{
    public abstract class BaseHealth : ContextComponent
    {
        public abstract UnityEvent<float> OnHeal { get; }

        public abstract UnityEvent<Combat.Damage> OnDamageTaken { get; }

        public abstract UnityEvent OnDied { get; }
        
        public abstract float CurrentValue { get; }
        
        public abstract float CurrentValueNormalized { get; } 

        public abstract float MaxValue { get; set; }
        
        public abstract float StartValueNormalized { get; set; }

        public abstract bool IsDead { get; }

        public bool IsAlive => !IsDead;
        
        public abstract bool IsVulnerable { get; set; }

        public abstract StatDef MaxHealthStatDef { get; }

        public abstract float ChangeHealth(float health);
        
        public abstract void TakeDamage(Combat.Damage damageObj, bool ignoreInvulnerability = false);

        public abstract void Heal(float health);
        
        public abstract void Kill(bool ignoreInvulnerability = true);
        
    }
}
