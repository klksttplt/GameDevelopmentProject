using Logic.Common;
using Logic.Stats;
using UnityEngine;

namespace Logic.Combat
{
    public abstract class AttackController : ContextComponent
    {
        // Fields: Editor
        
        [SerializeField, Header("Stats")] 
        protected StatData damageValueData;
        [SerializeField, Header("Stats")] 
        protected StatData attackRechargeTimeData;
        
        // Fields: Internal State
        
        protected StatValueProvider damageValueProvider;
        protected float damageValue;
        
        protected StatValueProvider attackRechargeTimeProvider;
        protected float attackRechargeTime;

        // Public API
        
        public StatDef DamageStatDef => damageValueData.Def;
        public StatDef AttackRechargeTimeDef => attackRechargeTimeData.Def;

        public abstract void Attack();

        // Methods: Lifecycle

        public override void Awake()
        {
            base.Awake();
            var damageProvidedData = GetStatValue(DamageStatDef);
            damageValueProvider = damageProvidedData ?? damageValueData.DefaultBaseValueProvider;
            damageValue = damageValueProvider.BaseValue;
            
            var attackRechargeProvidedData = GetStatValue(AttackRechargeTimeDef);
            attackRechargeTimeProvider = attackRechargeProvidedData ?? attackRechargeTimeData.DefaultBaseValueProvider;
            attackRechargeTime = attackRechargeTimeProvider.BaseValue;
        }
    }
}
