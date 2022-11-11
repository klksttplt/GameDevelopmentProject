using Logic.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Logic.Damage
{
    public abstract class GenericHealth : BaseHealth
    {
        // Events
     
        public override UnityEvent<float> OnHeal { get; } = new UnityEvent<float>();
        
        public override UnityEvent<Combat.Damage> OnDamageTaken { get; } = new UnityEvent<Combat.Damage>();

        public override UnityEvent OnDied { get; } = new UnityEvent();
        
        // Fields: Editor
        
        [SerializeField] 
        protected StatData maxHealthStatData;
        
        // Fields: Internal State

        private StatValueProvider healthValueProvider;
        private float maxHealth;
        private bool isDead;
        private bool isVulnerable = true;
        private float startValueNormalized = 1f;
        
        //Public API
        
        public override float CurrentValue => GetCurrentHealth();
        public override float CurrentValueNormalized => CurrentValue / maxHealth;
        public override bool IsDead => isDead;
        public override StatDef MaxHealthStatDef => maxHealthStatData.Def;

        public override float MaxValue
        {
            get =>maxHealth; 
            set => maxHealth = value;
        }

        public override float StartValueNormalized
        {
            get => startValueNormalized; 
            set => startValueNormalized = Mathf.Clamp01(value);
        }

        public override bool IsVulnerable
        {
            get => isVulnerable; 
            set
            {
                if (isVulnerable != value) isVulnerable = value;
            }
        }

        public override float ChangeHealth(float health)
        {
            var newValue = Mathf.Clamp(health, 0f, maxHealth);
            SetCurrentHealth(newValue);
            isDead = newValue <= 0f;
            return newValue;
        }

        public override void TakeDamage(Combat.Damage damage, bool ignoreInvulnerability = false)
        {
            var currentHealth = GetCurrentHealth();
            if (currentHealth > 0f && (isVulnerable || ignoreInvulnerability))
            {
                var damagedHealthValue = currentHealth - damage.amount;
                currentHealth = ChangeHealth(damagedHealthValue);
                
                OnDamageTaken.Invoke(damage);
                if (currentHealth <= 0f) OnDied.Invoke();
            }
        }

        public override void Heal(float health)
        {
            if (!IsDead)
            {
                var currentHealth = GetCurrentHealth();
                ChangeHealth(currentHealth + health);
                OnHeal.Invoke(health);
            }
        }

        public override void Kill(bool ignoreInvulnerability = true)
        {
            var damage = new Combat.Damage
            {
                amount = GetCurrentHealth()
            };
            TakeDamage(damage, ignoreInvulnerability);
        }
        
        // Methods: Lifecycle
        
        public override void Awake()
        {
            base.Awake();
            var providedData = GetStatValue(MaxHealthStatDef);
            healthValueProvider = providedData ?? maxHealthStatData.DefaultBaseValueProvider;
            maxHealth = healthValueProvider.BaseValue;
            ChangeHealth(1f);
        }
        
        // Methods: Internal State
        
        protected abstract float GetCurrentHealth();
        protected abstract void SetCurrentHealth(float value);
    }
}
